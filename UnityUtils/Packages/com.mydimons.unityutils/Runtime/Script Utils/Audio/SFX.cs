using UnityEngine;
using UnityUtils.ScriptUtils.Audio;

/// <summary>
/// Used in <see cref="SfxManager"/> to play audio clips with some set parameters.
/// </summary>
[System.Serializable]
public class SFX {
  public SFX(AudioClip[] audioClips) {
    this.audioClips = audioClips;
  }

  public SFX(AudioClip audioClip) {
    this.audioClips = new AudioClip[] { audioClip };
  }

  /// <summary>
  /// Sets the <see cref="minVolume"/> and <see cref="maxVolume"/> for this <see cref="SFX"/>. Audio gets randomly played between the min and max volume.
  /// </summary>
  public void SetVolume(float minVolume, float maxVolume) {
    this.minVolume = minVolume;
    this.maxVolume = maxVolume;
  }

  /// <summary>
  /// Sets the <see cref="pitch"/> and <see cref="pitchVariance"/> for this <see cref="SFX"/>
  /// </summary>
  public void SetPitch(float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE, float pitch = 1) {
    this.pitch = pitch;
    this.pitchVariance = pitchVariance;
  }

  /// <summary>
  /// Sets the <see cref="localPosition"/> and <see cref="spacialBlend"/> for this <see cref="SFX"/>
  /// </summary>
  /// <param name="localPosition"></param>
  /// <param name="spacialBlend"></param>
  public void SetSpacialAudio(Vector3 localPosition, float spacialBlend = 1) {
    this.localPosition = localPosition;
    this.spacialBlend = spacialBlend;
  }

  /// <summary>
  /// Sets the <see cref="audioSource"/> of this <see cref="SFX"/>
  /// </summary>
  public void SetAudioSource(AudioSource audioSource) {
    this.audioSource = audioSource;
  }

  /// <summary>
  /// Sets the <see cref="name"/> of this <see cref="SFX"/>
  /// </summary>
  public void SetName(string name) {
    this.name = name;
  }

  /// <summary>
  /// Sets the <see cref="parent"/> of this <see cref="SFX"/>
  /// </summary>
  public void SetParent(Transform parent) {
    this.parent = parent;
  }

  /// <summary>
  /// Randomly play one of these clips when this SFX is played.
  /// </summary>
  /// <remarks>Only set 1 AudioClip if you do not want to randomize the clips</remarks>
  public AudioClip[] audioClips;

  /// <summary>
  /// If null, will create a new object and add an audio source. Otherwise will play on the set audio source.
  /// </summary>
  public AudioSource audioSource = null;

  /// <summary>
  /// <see cref="AudioManager.VolumeType"/> to play this SFX as. 
  /// </summary>
  /// <remarks>see <see cref="AudioManager.CalculateVolumeBasedOnType(float, AudioManager.VolumeType)"/> to get more info.</remarks>
  [Header("Volume")]
  public AudioManager.VolumeType audioType = AudioManager.VolumeType.Sfx;

  /// <summary>
  /// Maximum volume this SFX can be played at. 
  /// </summary>
  /// <remarks>
  /// Audio gets randomly played between the min and max volume.
  /// <para/>
  /// Defaults to 1
  /// </remarks>
  [Range(0, 1)]
  public float maxVolume = AudioManager.MAX_AUDIO_VOLUME;

  /// <summary>
  /// Minimum volume this SFX can be played at. Audio gets randomly played between the min and max volume. 
  /// </summary>
  /// <remarks>
  /// Audio gets randomly played between the min and max volume.
  /// <para/>
  /// Defaults to <see cref="AudioManager.MAX_AUDIO_VOLUME"/>.
  /// </remarks>
  [Range(0, 1)]
  public float minVolume = AudioManager.MAX_AUDIO_VOLUME;

  /// <summary>
  /// Using the <see cref="pitch"/>, adds an offset by -/+ <see cref="pitchVariance"/>.
  /// </summary>
  /// <remarks>
  /// Used to make clips not feel so repetative.
  /// <para/>
  /// Defaults to <see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/>.
  /// </remarks>
  [Header("Pitch")]
  public float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

  /// <summary>
  /// The pitch the <see cref="AudioClip"/> gets played at.
  /// </summary>
  /// <remarks>Defaults to 1.0</remarks>
  public float pitch = 1.0f;

  /// <summary>
  /// The <see cref="Vector3"/> position to play this at. Used in turn with <see cref="spacialBlend"/>.
  /// </summary>
  [Header("Spacial Audio")]
  public Vector3 localPosition = Vector3.zero;
  /// <summary>
  /// Value between 0 and 1 (inclusive) that decides if an <see cref="AudioClip"/> is spacial audio. 0 is no spacial audio, 1 is spacial audio.
  /// </summary>
  public float spacialBlend = 0.0f;

  /// <summary>
  /// If true, will destroy the clip when its done playing. Otherwise it will not.
  /// </summary>
  [Header("Clip Destruction")]
  public bool destroyOnClipEnd = true;
  /// <summary>
  /// If true, will wait for the clip length in realtime before getting destroyed.
  /// </summary>
  public bool useRealtimeToDestroy = true;
  /// <summary>
  /// IS NOT IMPLIMENTED YET. If true, will not get destroyed when the scene changes.
  /// </summary>
  public bool scenePersistant = false;

  [Header("Organization")]
  /// <summary>
  /// The name of this SFX when instantiated.
  /// </summary>
  public string name = "[UnityUtils] SFX";

  /// <summary>
  /// The transform to set the instantiated <see cref="SFX"/> clip's parent as.
  /// </summary>
  /// <remarks>Useful for orginization purposes.</remarks>
  public Transform parent = null;
}