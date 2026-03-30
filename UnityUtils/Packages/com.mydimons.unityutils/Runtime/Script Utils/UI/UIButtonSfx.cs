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
    public SFX hoverEnterSFX = SFX.Create2dSFX();

    [Space(5)]

    /// <summary>
    /// Sound to play when the button is no longer being hovered.
    /// </summary>
    public SFX hoverExitSFX = SFX.Create2dSFX();

    [Space(5)]

    /// <summary>
    /// Sound to play when the button is clicked
    /// </summary>
    public SFX clickSFX = SFX.Create2dSFX();

    private bool sceneLoadTriggered;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> depending on the <see cref="UIButtonDebugSettings"/>
    /// </summary>
    [Header("Debug.Logs()")]
    public UIButtonDebugSettings debugLogs;

    /// <summary>
    /// Will output a <see cref="Debug.LogWarning(object)"/> when a hover enter, exit, or click SFX tries to be played but no SFX clip is found.
    /// </summary>
    public bool logEmptySFX;

    public void OnPointerEnter(PointerEventData eventData) {
      if (hoverEnterSFX.HasAudioClips()) {
        SFXManager.PlaySFX(hoverEnterSFX);

        LogAny();
        if (debugLogs.logIn)
          Debug.Log("Played hover enter SFX on button!");
      } else if (logEmptySFX)
        Debug.LogWarning("No hover enter SFX on button!");
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (sceneLoadTriggered)
        return;

      if (hoverExitSFX.HasAudioClips()) {
        SFXManager.PlaySFX(hoverExitSFX);

        LogAny();
        if (debugLogs.logIn)
          Debug.Log("Played hover exit SFX on button!");
      } else if (logEmptySFX)
        Debug.LogWarning("No hover exit SFX on button!");
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (GetComponent<UIButtonSceneSwitcher>()) {
        sceneLoadTriggered = true;
        return;
      }

      if (clickSFX.HasAudioClips()) {
        SFXManager.PlaySFX(clickSFX);

        LogAny();
        if (debugLogs.logIn)
          Debug.Log("Played hover click SFX on button!");
      } else if (logEmptySFX)
        Debug.LogWarning("No click SFX on button!");
    }

    private void LogAny() {
      if (debugLogs.logAny) {
        Debug.Log("Played SFX on UI button!");
      }
    }
  }
}
