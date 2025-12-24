using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.Audio
{
    [RequireComponent(typeof(Slider))]
    public class AudioSlider : MonoBehaviour
    {
        /// Type of audio volume to modify on update.
        [SerializeField] private AudioManager.VolumeType volumeType;

        [Space(10)]

        /// If true, this will print a debug log of the updated volume on update. Warning: While being used, this will output lots of Debug.Logs.
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

            if (debugLogs)
                Debug.Log("Set " + volumeType + " Volume to: " + volume);
        }
    }
}