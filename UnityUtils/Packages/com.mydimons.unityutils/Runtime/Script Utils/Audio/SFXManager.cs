using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.Audio {
  public static class SFXManager {
    /// <summary>
    /// Plays the provided <see cref="SFX"/> using the <paramref name="sfx"/> clips parameters
    /// </summary>
    /// <param name="sfx">The <see cref="SFX"/> object to play</param>
    public static void PlaySFX(SFX sfx) {
      PlaySFXClip(sfx);
    }

    private static void PlaySFXClip(SFX sfx) {
      if (!sfx.HasAudioClips()) {
        Debug.Log("No audio clips provided for this SFX. Cannot play SFX.");
        return;
      }

      GameObject sfxObject = new GameObject(sfx.name);
      AudioSource audioSource = sfx.audioSource != null ? sfx.audioSource : sfxObject.AddComponent<AudioSource>();

      // Get random AudioClip
      int randomClipIndex = Random.Range(0, sfx.audioClips.Length - 1);
      AudioClip audioClip = sfx.audioClips[randomClipIndex];

      sfxObject.transform.parent = sfx.parent.transform;
      audioSource.spatialBlend = sfx.spacialSettings.spacialBlend;
      audioSource.pitch = AudioManager.CalculatePitchVariance(sfx.pitchSettings.pitchVariance, sfx.pitchSettings.pitch);

      // Play clip
      audioSource.clip = audioClip;
      audioSource.volume = AudioManager.CalculateVolumeBasedOnType(sfx.volumeSettings.volume, sfx.volumeSettings.volumeType);

      audioSource.Play();

      // Destroy clip
      if (sfx.destructionSettings.destroyOnClipEnd) {
        float destroyTime = AudioManager.CalculateClipLength(audioClip.length, audioSource.pitch);
        ObjectDelays.Delay(() => Object.Destroy(sfxObject), destroyTime, sfx.destructionSettings.useRealtimeToDestroy);
      }
    }
  }
}