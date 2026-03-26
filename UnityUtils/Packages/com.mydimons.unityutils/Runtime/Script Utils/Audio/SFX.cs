using UnityEngine;
using UnityUtils.ScriptUtils.Audio;

/// <summary>
/// 
/// </summary>
public class SFX {
  /// <summary>
  /// Randomly play one of these clips when this SFX is played.
  /// </summary>
  /// <remarks>Only set 1 AudioClip if you do not want to randomize the clips</remarks>
  public AudioClip[] clips;

  /// <summary>
  /// Maximum volume this SFX can be played at. 
  /// </summary>
  /// <remarks>
  /// Audio gets randomly played between the min and max volume.
  /// <para/>
  /// Defaults to <see cref="AudioManager.MAX_AUDIO_VOLUME"/>.
  /// </remarks>
  public float maxVolume = AudioManager.MAX_AUDIO_VOLUME;

  /// <summary>
  /// Minimum volume this SFX can be played at. Audio gets randomly played between the min and max volume. 
  /// </summary>
  /// <remarks>
  /// Audio gets randomly played between the min and max volume.
  /// <para/>
  /// Defaults to <see cref="AudioManager.MAX_AUDIO_VOLUME"/>.
  /// </remarks>
  public float minVolume = AudioManager.MAX_AUDIO_VOLUME;

  /// <summary>
  /// <see cref="AudioManager.VolumeType"/> to play this SFX as. 
  /// </summary>
  /// <remarks>see <see cref="AudioManager.CalculateVolumeBasedOnType(float, AudioManager.VolumeType)"/> to get more info.</remarks>
  public AudioManager.VolumeType audioType = AudioManager.VolumeType.Sfx;

  /// <summary>
  /// The name of this SFX when instantiated.
  /// </summary>
  public string name = "[UnityUtils] SFX";

  /// <summary>
  /// The <see cref="Vector3"/> position to play this at. Used in turn with <see cref="spacialAudio"/>.
  /// </summary>
  public Vector3 positition = Vector3.zero;
  /// <summary>
  /// Value between 0 and 1 (inclusive) that decides if an <see cref="AudioClip"/> is spacial audio. 0 is no spacial audio, 1 is spacial audio.
  /// </summary>
  public float spacialAudio = 0.0f;

  /// <summary>
  /// The transform to set the instantiated <see cref="SFX"/> clip's parent as.
  /// </summary>
  /// <remarks>Useful for orginization purposes.</remarks>
  public Transform parent = null;

  /// <summary>
  /// Using the <see cref="pitch"/>, adds an offset by -/+ <see cref="pitchVariance"/>.
  /// </summary>
  /// <remarks>
  /// Used to make clips not feel so repetative.
  /// <para/>
  /// Defaults to <see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/>.
  /// </remarks>
  public float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

  /// <summary>
  /// The pitch the <see cref="AudioClip"/> gets played at.
  /// </summary>
  /// <remarks>Defaults to 1.0</remarks>
  public float pitch = 1.0f;
}
