using System;
using System.Collections.Generic;

namespace UnityUtils.ScriptUtils.SaveSystem {
  public class SaveSlot {
    /// <summary>
    /// The save slot name
    /// </summary>
    public readonly string saveSlotName;

    /// <summary>
    /// The last time this save slot was saved
    /// </summary>
    public DateTime lastTimeSaved;

    /// <summary>
    /// Instances of the <see cref="SaveData"/> linked to this object
    /// </summary>
    private readonly List<SaveData> saveDatas = new();

    public SaveSlot(string saveSlotName) {
      this.saveSlotName = saveSlotName;

      // Create save slot save data
      string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlotName, SaveSystemUtils.SAVE_SLOT_SAVE_FILE_NAME);
      AddSaveData(SaveSystemManager.CreateSaveData<SaveSlotSaveData>(path));
    }

    /// <summary>
    /// Adds a data instance to <see cref="saveDatas"/>
    /// </summary>
    /// <param name="dataInstance">instance to add</param>
    public void AddSaveData(SaveData dataInstance) {
      saveDatas.Add(dataInstance);
      dataInstance.SetSaveSlot(this);
    }


    public void AddSaveData(List<SaveData> saveDataList) {
      foreach (SaveData saveData in saveDataList) {
        AddSaveData(saveData);
      }
    }

    public void SetSaveDataSlot(List<SaveData> saveDataList) {
      foreach (SaveData saveData in saveDataList) {
        saveData.SetSaveSlot(this);
      }
    }

    public List<SaveData> GetSaveDatas() {
      return saveDatas;
    }

    public void SaveAllSaveDatas() {
      foreach (SaveData saveData in saveDatas)
        saveData.Save();
    }

    public void LoadAllSaveDatas() {
      foreach (SaveData saveData in saveDatas)
        saveData.Load();
    }
  }
}