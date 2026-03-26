using Newtonsoft.Json;
using System;

namespace UnityUtils.ScriptUtils.SaveSystem {
  /// <summary>
  /// Implimenting this class means you must have "[System.Serializable]" above your class name
  /// </summary>
  [Serializable]
  public abstract class SaveData {
    /// <summary>
    /// Save file name to write files to
    /// </summary>
    public string saveFileName;

    /// <summary>
    /// The save slot this data belongs to
    /// </summary>
    [JsonIgnore]
    public SaveSlot saveSlot;

    /// <summary>
    /// Set data variables for the save data
    /// </summary>
    /// <param name="saveFileName">File name to set to</param>
    public void SetData(string saveFileName) {
      this.saveFileName = saveFileName;
    }

    public void SetSaveSlot(SaveSlot saveSlot) {
      this.saveSlot = saveSlot;
    }

    /// <summary>
    /// Any actions to preform when saving
    /// </summary>
    public virtual void Save() { }

    /// <summary>
    /// Any actions to preform when loading
    /// </summary>
    public virtual void Load() { }
  }
}
