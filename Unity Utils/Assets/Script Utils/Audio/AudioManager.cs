using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace UnityUtils.ScriptUtils.Audio {
    /// <summary>
    /// Holds variables/functions for other audio scripts (Such as global volume)
    /// </summary>
    public static class AudioManager
    {
        /// Holds different audio types for volume calculations
        public enum AudioType
        {
            sfx,
            music,
            global,
            custom
        }

        static Dictionary<AudioType, float> audioVolumes = new Dictionary<AudioType, float>()
        {
            { AudioType.sfx,    1f },
            { AudioType.music,  1f },
            { AudioType.global, 1f },
            { AudioType.custom, 1f },
        };

        /// <summary>
        /// Gets the current volume level for the specified audio type.
        /// </summary>
        /// <returns>The volume level for the specified audio type, as a value between 0.0 (silent) and 1.0 (maximum volume).</returns>
        public static float GetVolume(AudioType audioType)
        {
            return audioVolumes[audioType];
        }

        /// <summary>
        /// Adjusts the volume for the specified audio type by adding the given value to its current volume.
        /// </summary>
        public static void ModifyVolume(AudioType audioType, float volume)
        {
            SetVolume(audioType, GetVolume(audioType) + volume);
        }

        /// <summary>
        /// Sets the volume level for the specified audio type to the new volume. Clamps to a range between 0-1 inclusive
        /// </summary>
        public static void SetVolume(AudioType audioType, float volume)
        {
            audioVolumes[audioType] = Mathf.Clamp01(volume);
        }

        /// <summary>
        /// Does multiplication to volume types to the get right audio levels.
        /// </summary>
        /// <returns>
        /// Proper volume level based on audio type.
        /// </returns>
        public static float CalculateVolumeBasedOnType(float volume, AudioType audioType) => audioType switch
        {
            AudioType.sfx    => MultiplyByGlobalVolume(volume) * GetVolume(AudioType.sfx),
            AudioType.music  => MultiplyByGlobalVolume(volume) * GetVolume(AudioType.music),
            AudioType.global => MultiplyByGlobalVolume(volume),
            AudioType.custom => MultiplyByGlobalVolume(volume) * GetVolume(AudioType.custom),
            _                => MultiplyByGlobalVolume(volume),
        };

        private static float MultiplyByGlobalVolume(float volume) => volume * GetVolume(AudioType.global);

        /// <summary>
        /// Calculates the effective playback duration of an audio clip after adjusting for pitch.
        /// </summary>
        /// <returns>Adjusted clip length based on pitch (clipLength / pitch).</returns>
        public static float CalculateClipLength(float clipLength, float pitch) => clipLength / Math.Abs(pitch);

        /// <summary>
        /// Calculates the pitch adjustment factor needed to play an audio clip at a specified duration.
        /// </summary>
        /// <returns>Pitch factor to achieve the desired playback time (clipLength * time).</returns>
        public static float CalculateClipPitchWithLength(float clipLength, float time) => clipLength * time;

        /// <summary>
        /// Calculates the pitch variance to add randomness to playback.
        /// </summary>
        /// <returns>Random number between 1 - pitchVariance and 1 + pitchVariance.</returns>
        public static float CalculatePitchVariance(float pitchVariance) => UnityEngine.Random.Range(1 - pitchVariance, 1 + pitchVariance);
    }
}