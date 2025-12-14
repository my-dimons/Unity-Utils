using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.Audio
{
    [RequireComponent(typeof(Slider))]
    public class AudioSlider : MonoBehaviour
    {
        /// Type of audio volume to modify on update.
        public AudioManager.VolumeType volumeType;

        [Space(10)]

        /// If true, this will print a debug log of the updated volume on update.
        public bool debugLogs = true;

        void Start()
        {
            Slider slider = GetComponent<Slider>();

            slider.onValueChanged.AddListener(OnSliderValueChanged);
            slider.value = AudioManager.GetVolume(volumeType);
        }

        private void OnSliderValueChanged(float volume)
        {
            AudioManager.SetVolume(volumeType, volume);
            Debug.Log("Set " + volumeType + " Volume to: " + volume);
        }
    }
}