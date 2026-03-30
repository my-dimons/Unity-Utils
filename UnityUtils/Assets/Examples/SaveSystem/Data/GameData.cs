using System;
using UnityUtils.ScriptUtils.SaveSystem;

[Serializable]
public class GameData : SaveData {
  public int intValue;
  public string stringValue;
  public float[] color = new float[4];
}
