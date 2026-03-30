using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveTesting : MonoBehaviour, ISaveableData {
  public int val1 = 2;
  public string val2 = "Unsaved";

  public TextMeshProUGUI intText;
  public TextMeshProUGUI stringText;

  public Image objectTest;

  public void SaveData<T>(T data) where T : SaveData {
    if (data is GameData save) {
      save.intValue = val1;
      save.stringValue = val2;

      save.color[0] = objectTest.color.r;
      save.color[1] = objectTest.color.g;
      save.color[2] = objectTest.color.b;
      save.color[3] = objectTest.color.a;
    }
  }

  public void LoadData<T>(T data) where T : SaveData {
    if (data is GameData save) {
      val1 = save.intValue;
      val2 = save.stringValue;

      objectTest.color = new Color(save.color[0], save.color[1], save.color[2], save.color[3]);
      Debug.Log(objectTest.color.r);
    }
  }

  private void Update() {
    intText.text = "val1: " + val1;
    stringText.text = "val2: " + val2;
  }
}