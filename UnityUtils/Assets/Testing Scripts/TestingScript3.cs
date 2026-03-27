using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

public class TestingScript3 : MonoBehaviour {
  public GameObject mat3;

  public Color flashColor;

  private void Start() {
    Invoke(nameof(FlashColors), 1f);
  }

  private void FlashColors() {
    mat3.GetComponent<ColorFlashManager>().Flash();
  }
}
