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
    public AudioClip hoverEnterSfx;
    /// <summary>
    /// Volume to play <see cref="hoverEnterSfx"/> at.
    /// </summary>
    public float hoverEnterVolume = AudioManager.MAX_AUDIO_VOLUME;

    [Space(5)]

    /// <summary>
    /// Sound to play when the button is no longer being hovered.
    /// </summary>
    public AudioClip hoverExitSfx;
    /// <summary>
    /// Volume to play <see cref="hoverExitSfx"/> at.
    /// </summary>
    public float hoverExitVolume = AudioManager.MAX_AUDIO_VOLUME;

    [Space(5)]

    /// <summary>
    /// Sound to play when the button is clicked
    /// </summary>
    public AudioClip clickSfx;

    /// <summary>
    /// Volume to play <see cref="clickSfx"/> at.
    /// </summary>
    public float clickVolume = AudioManager.MAX_AUDIO_VOLUME;


    /// <summary>
    /// Random variance to play all audio clips at.
    /// </summary>
    [Header("Adjustable Values")]
    public float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

    /// <summary>
    /// Type of audio to use to get proper audio levels.
    /// </summary>
    public AudioManager.VolumeType volumeType = AudioManager.VolumeType.Sfx;

    private bool sceneLoadTriggered;

    /// <summary>
    /// Will output a <see cref="Debug.LogWarning(object)"/> when a hover enter, exit, or click SFX tries to be played but no SFX clip is found.
    /// </summary>
    [Header("Debug")]
    public bool logEmptySfx;

    public void OnPointerEnter(PointerEventData eventData) {
      if (hoverEnterSfx != null)
        SfxManager.PlaySfxAudioClip(hoverEnterSfx, hoverEnterVolume, pitchVariance, volumeType);
      else if (logEmptySfx)
        Debug.LogWarning("No hover enter SFX on button!");
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (sceneLoadTriggered)
        return;

      if (hoverExitSfx != null)
        SfxManager.PlaySfxAudioClip(hoverExitSfx, hoverExitVolume, pitchVariance, volumeType);
      else if (logEmptySfx)
        Debug.LogWarning("No hover exit SFX on button!");
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (GetComponent<UIButtonSceneSwitcher>()) {
        sceneLoadTriggered = true;
        return;
      }

      if (clickSfx != null)
        SfxManager.PlaySfxAudioClip(clickSfx, clickVolume, pitchVariance, volumeType);
      else if (logEmptySfx)
        Debug.LogWarning("No click SFX on button!");
    }
  }
}
