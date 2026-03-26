using System;
using UnityUtils.ScriptUtils.SaveSystem;

[Serializable]
public class GameData : SaveData {
  public int intValue;
  public string stringValue;
  public float[] positionValue = new float[3];
}
