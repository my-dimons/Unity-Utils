using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  [System.Serializable]
  public class SFXDestructionSettings {
    /// <summary>
    /// If true, will destroy the clip when its done playing. Otherwise it will not.
    /// </summary>
    public bool destroyOnClipEnd = true;
    /// <summary>
    /// If true, will wait for the clip length in realtime before getting destroyed.
    /// </summary>
    public bool useRealtimeToDestroy = true;
    /// <summary>
    /// IS NOT IMPLIMENTED YET. If true, will not get destroyed when the scene changes.
    /// </summary>
    public bool scenePersistant = false;

    /// <summary>
    /// Set the settings on this <see cref="SFXDestructionSettings"/>.
    /// </summary>
    public SFXDestructionSettings SetDestructionSettings(bool destroyOnClipEnd = true, bool useRealtimeToDestroy = true, bool scenePersistant = false) {
      this.destroyOnClipEnd = destroyOnClipEnd;
      this.useRealtimeToDestroy = useRealtimeToDestroy;
      this.scenePersistant = scenePersistant;
      return this;
    }
  }

  [System.Serializable]
  public class SFX3dSettings {
    /// <summary>
    /// The <see cref="Vector3"/> position to play this at. Used in turn with <see cref="spacialBlend"/>.
    /// </summary>
    public Vector3 localPosition = Vector3.zero;
    /// <summary>
    /// Value between 0 and 1 (inclusive) that decides if an <see cref="AudioClip"/> is spacial audio. 0 is no spacial audio, 1 is spacial audio.
    /// </summary>
    [Range(0f, 1f)]
    public float spacialBlend;

    /// <summary>
    /// Sets the <see cref="localPosition"/> and <see cref="spacialBlend"/> for this <see cref="SFX"/>
    /// </summary>
    /// <param name="localPosition"></param>
    /// <param name="spacialBlend"></param>
    public SFX3dSettings SetSpacialAudio(Vector3 localPosition, float spacialBlend = 1) {
      this.localPosition = localPosition;
      this.spacialBlend = spacialBlend;
      return this;
    }
  }

  [System.Serializable]
  public class SFXPitchSettings {
    /// <summary>
    /// Using the <see cref="pitch"/>, adds an offset by -/+ <see cref="pitchVariance"/>.
    /// </summary>
    /// <remarks>
    /// Used to make clips not feel so repetative.
    /// <para/>
    /// Defaults to <see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/>.
    /// </remarks>
    public float pitchVariance;

    /// <summary>
    /// The pitch the <see cref="AudioClip"/> gets played at.
    /// </summary>
    /// <remarks>Defaults to 1.0</remarks>
    public float pitch;

    /// <summary>
    /// Sets the <see cref="pitch"/> and <see cref="pitchVariance"/> for this <see cref="SFX"/>
    /// </summary>
    public SFXPitchSettings SetPitch(float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE, float pitch = 1) {
      this.pitch = pitch;
      this.pitchVariance = pitchVariance;
      return this;
    }
  }

  [System.Serializable]
  public class SFXVolumeSettings {
    /// <summary>
    /// <see cref="AudioManager.VolumeType"/> to play this SFX as. 
    /// </summary>
    /// <remarks>see <see cref="AudioManager.CalculateVolumeBasedOnType(float, AudioManager.VolumeType)"/> to get more info.</remarks>
    public AudioManager.VolumeType volumeType;

    /// <summary>
    /// The volume this SFX will be played at. 
    /// </summary>
    /// <remarks>
    /// Defaults to 1
    /// </remarks>
    public float volume = 1;

    /// <summary>
    /// Sets the <see cref="volume"/> to <paramref name="volume"/> and <see cref="volumeType"/> to <paramref name="volumeType"/> for this <see cref="SFX"/>. Audio gets randomly played between the min and max volume.
    /// </summary>
    public SFXVolumeSettings SetVolume(float volume, AudioManager.VolumeType volumeType = AudioManager.VolumeType.SFX) {
      this.volume = volume;
      this.volumeType = volumeType;
      return this;
    }
  }
}