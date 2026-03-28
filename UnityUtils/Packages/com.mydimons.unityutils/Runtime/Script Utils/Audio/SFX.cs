using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  /// <summary>
  /// Used in <see cref="SFXManager"/> to play audio clips with some set parameters.
  /// </summary>
  [System.Serializable]
  public class SFX {
    /// <summary>
    /// Returns a new SFX with the following properties:
    /// <para>- <see cref="SFXVolumeSettings.volume"/> = 1</para>
    /// <para>- <see cref="SFXVolumeSettings.volumeType"/> = <see cref="AudioManager.VolumeType.Sfx"/></para>
    /// <para>- <see cref="SFXPitchSettings.pitch"/> = 1</para>
    /// <para>- <see cref="SFXPitchSettings.pitchVariance"/> = <see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/></para>
    /// <para>- <see cref="SFX3dSettings.localPosition"/> = <see cref="Vector3.zero"/></para>
    /// <para>- <see cref="SFX3dSettings.spacialBlend"/> = 0</para>
    /// <para>- <see cref="name"/> = "2D SFX [UnityUtils.ScriptUtils.Audio]"</para>
    /// <para>- <see cref="SFXDestructionSettings.destroyOnClipEnd"/> = true</para>
    /// <para>- <see cref="SFXDestructionSettings.useRealtimeToDestroy"/> = true</para>
    /// </summary>
    public static SFX Create2dSFX() {
      return new SFX {
        volumeSettings = new SFXVolumeSettings().SetVolume(1, AudioManager.VolumeType.Sfx),
        pitchSettings = new SFXPitchSettings().SetPitch(AudioManager.DEFAULT_PITCH_VARIANCE, 1),
        spacialSettings = new SFX3dSettings().SetSpacialAudio(Vector3.zero, 0),
        destructionSettings = new SFXDestructionSettings().SetDestructionSettings(),
        name = "2D SFX [UnityUtils.ScriptUtils.Audio]"
      };
    }

    /// <summary>
    /// Returns a new SFX with the following properties:
    /// <para>- <see cref="SFXVolumeSettings.volume"/> = 1</para>
    /// <para>- <see cref="SFXVolumeSettings.volumeType"/> = <see cref="AudioManager.VolumeType.Sfx"/></para>
    /// <para>- <see cref="SFXPitchSettings.pitch"/> = 1</para>
    /// <para>- <see cref="SFXPitchSettings.pitchVariance"/> = <see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/></para>
    /// <para>- <see cref="SFX3dSettings.localPosition"/> = <see cref="Vector3.zero"/></para>
    /// <para>- <see cref="SFX3dSettings.spacialBlend"/> = 1</para>
    /// <para>- <see cref="name"/> = "3D SFX [UnityUtils.ScriptUtils.Audio]"</para>
    /// <para>- <see cref="SFXDestructionSettings.destroyOnClipEnd"/> = true</para>
    /// <para>- <see cref="SFXDestructionSettings.useRealtimeToDestroy"/> = true</para>
    /// </summary>
    public static SFX Create3dSFX() {
      return new SFX {
        volumeSettings = new SFXVolumeSettings().SetVolume(1, AudioManager.VolumeType.Sfx),
        pitchSettings = new SFXPitchSettings().SetPitch(AudioManager.DEFAULT_PITCH_VARIANCE, 1),
        spacialSettings = new SFX3dSettings().SetSpacialAudio(Vector3.zero, 1),
        destructionSettings = new SFXDestructionSettings().SetDestructionSettings(),
        name = "3D SFX [UnityUtils.ScriptUtils.Audio]"
      };
    }

    /// <summary>
    /// Randomly play one of these clips when this SFX is played.
    /// </summary>
    /// <remarks>Only set 1 AudioClip if you do not want to randomize the clips</remarks>
    public AudioClip[] audioClips;

    /// <summary>
    /// If null, will create a new object and add an audio source. Otherwise will play on the set audio source.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The volume settings used when playing this SFX. See <see cref="SFXVolumeSettings"/> for more info.
    /// </summary>
    [Header("Settings")]
    public SFXVolumeSettings volumeSettings;
    /// <summary>
    /// The pitch settings used when playing this SFX. See <see cref="SFXPitchSettings"/> for more info.
    /// </summary>
    public SFXPitchSettings pitchSettings;
    /// <summary>
    /// The 3d settings used when playing this SFX. See <see cref="SFX3dSettings"/> for more info.
    /// </summary>
    public SFX3dSettings spacialSettings;
    /// <summary>
    /// The destruction settings used when playing this SFX. See <see cref="SFXDestructionSettings"/> for more info.
    /// </summary>
    public SFXDestructionSettings destructionSettings;

    [Header("Organization")]
    /// <summary>
    /// The name of this SFX when instantiated.
    /// </summary>
    public string name;

    /// <summary>
    /// The transform to set the instantiated <see cref="SFX"/> clip's parent as.
    /// </summary>
    /// <remarks>Useful for orginization purposes.</remarks>
    public Transform parent;

    /// <summary>
    /// Checks if this <see cref="SFX"/> has any <see cref="audioClips"/> to play.
    /// </summary>
    public bool HasAudioClips() {
      return audioClips != null && audioClips.Length > 0;
    }
  }
}