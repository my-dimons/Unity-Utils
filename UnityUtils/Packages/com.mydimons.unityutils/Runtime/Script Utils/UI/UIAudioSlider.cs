using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Audio;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Slider))]
  public class UIAudioSlider : MonoBehaviour {
    /// <summary>
    /// Type of audio volume to modify on update.
    /// </summary>
    public AudioManager.VolumeType volumeType;

    [Space(10)]

    /// <summary>
    /// If true, this will print a debug log of the updated volume on update. Warning: While being used, this will output lots of Debug.Logs.
    /// </summary>
    public bool logSliderValueChange = true;

    private Slider slider;

    void Start() {
      slider = GetComponent<Slider>();

      slider.onValueChanged.AddListener(OnSliderValueChanged);
      SetSliderValue();
    }

    private void OnSliderValueChanged(float volume) {
      AudioManager.SetVolume(volumeType, volume);

      if (logSliderValueChange)
        Debug.Log("Set " + volumeType + " Volume to: " + volume);
    }

    /// <summary>
    /// Sets the slider's volume to the current <see cref="volumeType"/> value
    /// </summary>
    public void SetSliderValue() {
      slider.value = AudioManager.GetVolume(volumeType);
    }
  }
}