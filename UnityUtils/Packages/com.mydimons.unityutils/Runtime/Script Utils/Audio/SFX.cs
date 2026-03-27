using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  /// <summary>
  /// Used in <see cref="SFXManager"/> to play audio clips with some set parameters.
  /// </summary>
  [System.Serializable]
  public class SFX {
    /// <summary>
    /// Returns a new SFX with default values
    /// <para/>
    /// <see cref="minVolume"/> + <see cref="maxVolume"/> = 1, <see cref="volumeType"/> = <see cref="AudioManager.VolumeType.Sfx"/>
    /// <para/>
    /// <see cref="pitch"/> = 1, <see cref="pitchVariance"/> = <see cref="AudioManager.DEFAULT_PITCH.VARIANCE"/>
    /// <para/>
    /// <see cref="localPosition"/> = <see cref="Vector3.zero"/>, <see cref="spacialBlend"/> = 0
    /// <para/>
    /// <see cref="name"/> = "[UnityUtils.ScriptUtils.Audio] SFX"
    /// <para/>
    /// <see cref="destroyOnClipEnd"/> = true, <see cref="useRealtimeToDestroy"/> = true"/>
    /// </summary>
    public static SFX CreateSFX() {
      return new SFX {
        destroyOnClipEnd = true,
        useRealtimeToDestroy = true,
      }.SetVolume(1)
      .SetPitch(AudioManager.DEFAULT_PITCH_VARIANCE, 1)
      .SetSpacialAudio(Vector3.zero, 0)
      .SetName("[UnityUtils.ScriptUtils.Audio] SFX");
    }

    /// <summary>
    /// Returns a new SFX with default values for music
    /// <para/>
    /// <see cref="minVolume"/> + <see cref="maxVolume"/> = 1, <see cref="volumeType"/> = <see cref="AudioManager.VolumeType.Music"/>
    /// <para/>
    /// <see cref="pitch"/> = 1, <see cref="pitchVariance"/> = 0
    /// <para/>
    /// <see cref="localPosition"/> = <see cref="Vector3.zero"/>, <see cref="spacialBlend"/> = 0
    /// <para/>
    /// <see cref="name"/> = "[UnityUtils.ScriptUtils.Audio] Music"
    /// <para/>
    /// <see cref="destroyOnClipEnd"/> = true, <see cref="useRealtimeToDestroy"/> = true"/>
    /// </summary>
    public static SFX CreateMusic() {
      return new SFX {
        destroyOnClipEnd = true,
        useRealtimeToDestroy = true,
      }.SetVolume(1, AudioManager.VolumeType.Music)
      .SetPitch(0, 1)
      .SetSpacialAudio(Vector3.zero, 0)
      .SetName("[UnityUtils.ScriptUtils.Audio] Music");
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
    /// <see cref="AudioManager.VolumeType"/> to play this SFX as. 
    /// </summary>
    /// <remarks>see <see cref="AudioManager.CalculateVolumeBasedOnType(float, AudioManager.VolumeType)"/> to get more info.</remarks>
    [Header("Volume")]
    public AudioManager.VolumeType volumeType;

    /// <summary>
    /// Maximum volume this SFX can be played at. 
    /// </summary>
    /// <remarks>
    /// Audio gets randomly played between the min and max volume.
    /// <para/>
    /// Defaults to 1
    /// </remarks>
    [Range(0, 1)]
    public float maxVolume;

    /// <summary>
    /// Minimum volume this SFX can be played at. Audio gets randomly played between the min and max volume. 
    /// </summary>
    /// <remarks>
    /// Audio gets randomly played between the min and max volume.
    /// <para/>
    /// Defaults to <see cref="AudioManager.MAX_AUDIO_VOLUME"/>.
    /// </remarks>
    [Range(0, 1)]
    public float minVolume;

    /// <summary>
    /// Using the <see cref="pitch"/>, adds an offset by -/+ <see cref="pitchVariance"/>.
    /// </summary>
    /// <remarks>
    /// Used to make clips not feel so repetative.
    /// <para/>
    /// Defaults to <see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/>.
    /// </remarks>
    [Header("Pitch")]
    public float pitchVariance;

    /// <summary>
    /// The pitch the <see cref="AudioClip"/> gets played at.
    /// </summary>
    /// <remarks>Defaults to 1.0</remarks>
    public float pitch;

    /// <summary>
    /// The <see cref="Vector3"/> position to play this at. Used in turn with <see cref="spacialBlend"/>.
    /// </summary>
    [Header("Spacial Audio")]
    public Vector3 localPosition = Vector3.zero;
    /// <summary>
    /// Value between 0 and 1 (inclusive) that decides if an <see cref="AudioClip"/> is spacial audio. 0 is no spacial audio, 1 is spacial audio.
    /// </summary>
    public float spacialBlend;

    /// <summary>
    /// If true, will destroy the clip when its done playing. Otherwise it will not.
    /// </summary>
    [Header("Clip Destruction")]
    public bool destroyOnClipEnd;
    /// <summary>
    /// If true, will wait for the clip length in realtime before getting destroyed.
    /// </summary>
    public bool useRealtimeToDestroy;
    /// <summary>
    /// IS NOT IMPLIMENTED YET. If true, will not get destroyed when the scene changes.
    /// </summary>
    public bool scenePersistant;

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
    /// Sets the <see cref="minVolume"/>, <see cref="maxVolume"/>, and <see cref="volumeType"/> for this <see cref="SFX"/>. Audio gets randomly played between the min and max volume.
    /// </summary>
    public SFX SetVolume(float minVolume, float maxVolume, AudioManager.VolumeType volumeType = AudioManager.VolumeType.Sfx) {
      this.minVolume = minVolume;
      this.maxVolume = maxVolume;
      this.volumeType = volumeType;
      return this;
    }

    /// <summary>
    /// Sets the <see cref="minVolume"/> and <see cref="maxVolume"/> to <paramref name="volume"/> and <see cref="volumeType"/> to <paramref name="volumeType"/> for this <see cref="SFX"/>. Audio gets randomly played between the min and max volume.
    /// </summary>
    public SFX SetVolume(float volume, AudioManager.VolumeType volumeType = AudioManager.VolumeType.Sfx) {
      return SetVolume(volume, volume, volumeType);
    }

    /// <summary>
    /// Sets the <see cref="pitch"/> and <see cref="pitchVariance"/> for this <see cref="SFX"/>
    /// </summary>
    public SFX SetPitch(float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE, float pitch = 1) {
      this.pitch = pitch;
      this.pitchVariance = pitchVariance;
      return this;
    }

    /// <summary>
    /// Sets the <see cref="localPosition"/> and <see cref="spacialBlend"/> for this <see cref="SFX"/>
    /// </summary>
    /// <param name="localPosition"></param>
    /// <param name="spacialBlend"></param>
    public SFX SetSpacialAudio(Vector3 localPosition, float spacialBlend = 1) {
      this.localPosition = localPosition;
      this.spacialBlend = spacialBlend;
      return this;
    }

    /// <summary>
    /// Sets the <see cref="audioSource"/> of this <see cref="SFX"/>
    /// </summary>
    public SFX SetAudioSource(AudioSource audioSource) {
      this.audioSource = audioSource;
      return this;
    }

    /// <summary>
    /// Sets the <see cref="name"/> of this <see cref="SFX"/>
    /// </summary>
    public SFX SetName(string name) {
      this.name = name;
      return this;
    }

    /// <summary>
    /// Sets the <see cref="parent"/> of this <see cref="SFX"/>
    /// </summary>
    public SFX SetParent(Transform parent) {
      this.parent = parent;
      return this;
    }

    /// <summary>
    /// Checks if this <see cref="SFX"/> has any <see cref="audioClips"/> to play.
    /// </summary>
    public bool HasAudioClips() {
      return audioClips != null && audioClips.Length > 0;
    }
  }
}