using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Audio;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    /// <summary>
    /// Sound to play when the button is hovered over
    /// </summary>
    [Header("Audio Clips & Volumes")]
    public SFX hoverEnterSfx = SFX.Create2DSFX();

    [Space(5)]

    /// <summary>
    /// Sound to play when the button is no longer being hovered.
    /// </summary>
    public SFX hoverExitSfx = SFX.Create2DSFX();

    [Space(5)]

    /// <summary>
    /// Sound to play when the button is clicked
    /// </summary>
    public SFX clickSfx = SFX.Create2DSFX();

    private bool sceneLoadTriggered;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> depending on the <see cref="UIButtonDebugSettings"/>
    /// </summary>
    [Header("Debug.Logs()")]
    public UIButtonDebugSettings debugLogs;

    /// <summary>
    /// Will output a <see cref="Debug.LogWarning(object)"/> when a hover enter, exit, or click SFX tries to be played but no SFX clip is found.
    /// </summary>
    public bool logEmptySfx;

    public void OnPointerEnter(PointerEventData eventData) {
      if (hoverEnterSfx != null) {
        SFXManager.PlaySFX(hoverEnterSfx);

        LogAny();
        if (debugLogs.logIn)
          Debug.Log("Played hover enter SFX on button!");
      } else if (logEmptySfx)
        Debug.LogWarning("No hover enter SFX on button!");
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (sceneLoadTriggered)
        return;

      if (hoverExitSfx != null) {
        SFXManager.PlaySFX(hoverExitSfx);

        LogAny();
        if (debugLogs.logIn)
          Debug.Log("Played hover exit SFX on button!");
      } else if (logEmptySfx)
        Debug.LogWarning("No hover exit SFX on button!");
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (GetComponent<UIButtonSceneSwitcher>()) {
        sceneLoadTriggered = true;
        return;
      }

      if (clickSfx != null) {
        SFXManager.PlaySFX(clickSfx);

        LogAny();
        if (debugLogs.logIn)
          Debug.Log("Played hover click SFX on button!");
      } else if (logEmptySfx)
        Debug.LogWarning("No click SFX on button!");
    }

    private void LogAny() {
      if (debugLogs.logAny) {
        Debug.Log("Played SFX on UI button!");
      }
    }
  }
}
