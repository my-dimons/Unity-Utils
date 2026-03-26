using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem {
  public static class SaveSystemManager {
    /// <summary>
    /// If true will output Debug.Log()'s on Save/Load
    /// </summary>
    public static bool outputLogs = true;

    /// <summary>
    /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
    /// </summary>
    /// <param name="saveSlot">Save slot to save data from <see cref="ISaveableData"/>'s</param>
    public static void SaveGame(SaveSlot saveSlot) {
      long startTime = DateTime.Now.Ticks;

      // Save saveData for each save saveData classType
      foreach (SaveData saveData in saveSlot.GetSaveDatas()) {
        // Put saveData from files to SaveData's
        SaveAllSaveableData(saveData);
        JsonSaveSystem.Save(saveData);

        if (outputLogs)
          SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveData.saveFileName));
      }

      long endTime = DateTime.Now.Ticks - startTime;

      if (outputLogs)
        Debug.Log($"Saved game data, took: {(endTime / TimeSpan.TicksPerMillisecond):N4}ms");
    }

    /// <summary>
    /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
    /// </summary>
    /// <param name="saveSlot">Save slot to load data from all the <see cref="ISaveableData"/>'s</param>
    public static void LoadGame(SaveSlot saveSlot) {
      long startTime = DateTime.Now.Ticks;

      // Inject save saveData into saveable files
      foreach (SaveData saveData in saveSlot.GetSaveDatas()) {
        SaveData data = JsonSaveSystem.LoadSingleSaveFile(saveData, saveSlot);

        LoadAllSaveableData(data);
      }

      long endTime = DateTime.Now.Ticks - startTime;

      if (outputLogs)
        Debug.Log($"Loaded game data, took: {(endTime / TimeSpan.TicksPerMillisecond):N4}ms");
    }

    /// <summary>
    /// Delete a save slot
    /// </summary>
    /// <param name="saveSlot">Save slot to delete</param>
    public static void DeleteSaveSlot(SaveSlot saveSlot) {
      JsonSaveSystem.Delete(saveSlot);
    }

    /// <summary>
    /// Gets all <see cref="ISaveableData"/> to call functions on
    /// </summary>
    /// <returns>List of all objects with <see cref="ISaveableData"/> attached</returns>
    public static List<ISaveableData> FindAllDataPersistanceObjects() =>
        UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<ISaveableData>()
            .ToList();

    /// <summary>
    /// Loads all save slots from the <see cref="SaveSystemUtils.SAVE_FILES_NAME"/> directory, if none exist one is created.
    /// </summary>
    /// <returns>Dictionary of the save slot name and <see cref="SaveSlot"/></returns>
    public static Dictionary<string, SaveSlot> LoadAllSaveSlots() {
      Dictionary<string, SaveSlot> saveSlotDictionary = new();

      // create save directory if it does not exist
      JsonSaveSystem.CreateRootSaveDataIfNotExisting();

      IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(SaveSystemUtils.GetSaveSlotRootPath()).EnumerateDirectories();

      // Loop through each save directory
      foreach (DirectoryInfo dirInfo in dirInfos) {
        string saveSlotName = dirInfo.Name;
        SaveSlot saveSlot;
        List<SaveData> saveDatas = new();

        string partialPath = SaveSystemUtils.GetSaveSlotPath(saveSlotName);

        // Loop through each file in directory
        foreach (FileInfo file in new DirectoryInfo(partialPath).GetFiles()) {
          string fullPath = Path.Combine(partialPath, file.Name);

          // Skip if no data
          if (!File.Exists(fullPath)) {
            Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + saveSlotName);
            continue;
          }

          saveDatas.Add(JsonSaveSystem.GetSaveData(JsonSaveSystem.GetJsonStringData(fullPath)));
        }

        saveSlot = new SaveSlot(saveSlotName);
        saveSlot.SetSaveDataSlot(saveDatas);
        saveSlot.AddSaveData(saveDatas);
        saveSlot.LoadAllSaveDatas();

        saveSlotDictionary.Add(saveSlot.saveSlotName, saveSlot);
      }

      return saveSlotDictionary;
    }

    /// <summary>
    /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
    /// </summary>
    /// <typeparam name="T">data type to encode, must inherit <see cref="SaveData"/></typeparam>
    /// <param name="fileName">The file name of the save object</param>
    /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
    public static SaveData CreateSaveData<T>(string fileName) where T : SaveData, new() {
      T saveData = new();
      saveData.SetData(fileName);

      return saveData;
    }

    /// <summary>
    /// Grabs the most recent save in a list of <see cref="SaveSlot"/>
    /// </summary>
    /// <param name="saveSlots">Save slots to sort through</param>
    /// <returns>Most recently saved slot</returns>
    public static SaveSlot GetMostRecentSave(List<SaveSlot> saveSlots) {
      return saveSlots
          .OrderByDescending(d => d.lastTimeSaved)
          .First();
    }

    /// <summary>
    /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on all the objects in <paramref name="saveTo"/>
    /// </summary>
    /// <param name="dataToSave">Data object to pass into the all the <paramref name="saveTo"/> <see cref="ISaveableData"/>'s</param>
    /// <param name="saveTo">All the <see cref="ISaveableData"/> objects that get <see cref="ISaveableData.SaveData{T}(T)"/> called on it, with <paramref name="dataToSave"/> passed into it</param>
    public static void SaveAllSaveableData(SaveData dataToSave, List<ISaveableData> saveTo) {
      foreach (ISaveableData saveable in saveTo) {
        saveable.SaveData(dataToSave);
      }
    }

    /// <summary>
    /// Calls <see cref="SaveAllSaveableData(SaveData, List{ISaveableData})"/>, and calls <see cref="FindAllDataPersistanceObjects"/> as the <see cref="List{ISaveableData}"/>
    /// </summary>
    /// <param name="dataToSave">Data object to pass into the all the <see cref="FindAllDataPersistanceObjects()"/> <see cref="ISaveableData"/>'s</param>
    public static void SaveAllSaveableData(SaveData dataToSave) {
      SaveAllSaveableData(dataToSave, FindAllDataPersistanceObjects());
    }


    /// <summary>
    /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on all the objects in <paramref name="loadFrom"/>
    /// </summary>
    /// <param name="dataToLoad">Data object to pass into the all the <paramref name="loadFrom"/> <see cref="ISaveableData"/>'s</param>
    /// <param name="loadFrom">All the <see cref="ISaveableData"/> objects that get <see cref="ISaveableData.LoadData{T}(T)"/> called on it, with <paramref name="dataToLoad"/> passed into it</param>
    public static void LoadAllSaveableData(SaveData dataToLoad, List<ISaveableData> loadFrom) {
      foreach (ISaveableData saveable in loadFrom) {
        saveable.LoadData(dataToLoad);

        if (outputLogs)
          SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(dataToLoad.saveFileName));
      }
    }

    /// <summary>
    /// Calls <see cref="SaveAllSaveableData(SaveData, List{ISaveableData})"/>, and calls <see cref="FindAllDataPersistanceObjects"/> as the <see cref="List{ISaveableData}"/>
    /// </summary>
    /// <param name="dataToLoad">Data object to pass into the all the <see cref="FindAllDataPersistanceObjects()"/> <see cref="ISaveableData"/>'s</param>
    public static void LoadAllSaveableData(SaveData dataToLoad) {
      LoadAllSaveableData(dataToLoad, FindAllDataPersistanceObjects());
    }
  }
}