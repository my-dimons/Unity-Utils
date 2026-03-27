using UnityEngine;
using UnityEngine.InputSystem;
using UnityUtils.ScriptUtils.Cameras;
using UnityUtils.ScriptUtils.Objects;
using UnityUtils.ScriptUtils.Objects.ColorFlash;
using UnityUtils.ScriptUtils.Objects.Modifiers;

public class TestingScript2 : MonoBehaviour {
  private void Start() {
    ModifierManager<float> modifierTest = new ModifierManager<float>();
    modifierTest.AddModifier(new ModifierData<float>(ModifierType.Flat, 1));
    modifierTest.AddModifier(new ModifierData<float>(ModifierType.Multiply, 5));
    modifierTest.AddModifier(new ModifierData<float>(ModifierType.Flat, 14));
    modifierTest.AddModifier(new ModifierData<float>(ModifierType.Divide, 3));
    modifierTest.AddModifier(new ModifierData<float>(ModifierType.Flat, 19));
    modifierTest.PrintModifierOrder();
    modifierTest.PrintModifiers();

    modifierTest.SortModifiers();

    modifierTest.PrintModifiers();

    Debug.Log("value of 1: " + modifierTest.CalculateModifiers(1));
    Debug.Log("value of 5: " + modifierTest.CalculateModifiers(5));

    modifierTest.AddTemporaryModifier(new ModifierData<float>(ModifierType.Flat, 3), 1);

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
