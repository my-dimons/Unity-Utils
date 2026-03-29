using UnityEngine;
using UnityEngine.InputSystem;
using UnityUtils.ScriptUtils.Cameras;
using UnityUtils.ScriptUtils.Objects;
using UnityUtils.ScriptUtils.Objects.ColorFlash;

public class TestingScript2 : MonoBehaviour {
  private void Start() {

    ObjectDelays.Delay(() => CameraShake.Screenshake(intensity: 5), 3);
    ObjectDelays.Delay(() => CameraShake.Screenshake(intensity: 5), 3.2f);
    ObjectDelays.Delay(() => CameraShake.Screenshake(intensity: 5), 4);
    ObjectDelays.Delay(() => GetComponent<ColorFlashManager>().Flash(), 1);
  }

  private void Update() {
    if (Keyboard.current.hKey.wasPressedThisFrame) {
      CameraShake.Screenshake();
    }
  }
}
