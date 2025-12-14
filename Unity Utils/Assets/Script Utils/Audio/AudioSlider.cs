using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Audio;

namespace UnityUtils.ScriptUtils.Audio
{
    public class AudioSlider : MonoBehaviour
    {
        public AudioManager.VolumeType volumeType;

        void Start()
        {
            this.GetComponent<Slider>().onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float volume)
        {
            AudioManager.SetVolume(volumeType, volume);
        }
    }
}