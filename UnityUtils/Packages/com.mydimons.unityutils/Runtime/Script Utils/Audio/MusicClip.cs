using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  [System.Serializable]
  public abstract class MusicClip : ScriptableObject {
    /// <summary>
    /// The music clip played.
    /// </summary>
    public AudioClip musicClip;

    /// <summary>
    /// The name of this music clip.
    /// </summary>
    public string musicName = "Music Clip [UnityUtils.ScriptUtils.Audio]";

    [Header("Audio Settings")]
    public SFXVolumeSettings volumeSettings = new SFXVolumeSettings().SetVolume(1, AudioManager.VolumeType.Music);
    public SFXPitchSettings pitchSettings = new SFXPitchSettings().SetPitch(0, 1);

    /// <returns>If true, the clip can be played when picking music</returns>
    public abstract bool CanBePlayed();
  }
}