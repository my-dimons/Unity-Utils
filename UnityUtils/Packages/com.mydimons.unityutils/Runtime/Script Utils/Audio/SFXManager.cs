using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.Audio {
  public static class SfxManager {
    /// <summary>
    /// Plays the provided <see cref="SFX"/> using the <paramref name="sfx"/> clips parameters
    /// </summary>
    /// <param name="sfx">The <see cref="SFX"/> object to play</param>
    public static void PlaySFX(SFX sfx) {
      PlaySFXClip(sfx);
    }

    private static void PlaySFXClip(SFX sfx) {
      GameObject sfxObject = new GameObject(sfx.name);
      AudioSource audioSource = sfx.audioSource != null ? sfx.audioSource : sfxObject.AddComponent<AudioSource>();

      // Get random AudioClip
      int randomClipIndex = Random.Range(0, sfx.audioClips.Length - 1);
      AudioClip audioClip = sfx.audioClips[randomClipIndex];

      float volume = Random.Range(sfx.minVolume, sfx.maxVolume);

      sfxObject.transform.parent = sfx.parent.transform;
      audioSource.spatialBlend = sfx.spacialBlend;
      audioSource.pitch = AudioManager.CalculatePitchVariance(sfx.pitchVariance, sfx.pitch);

      // Play clip
      audioSource.clip = audioClip;
      audioSource.volume = AudioManager.CalculateVolumeBasedOnType(volume, sfx.audioType);

      audioSource.Play();

      // Destroy clip
      if (sfx.destroyOnClipEnd) {
        float destroyTime = AudioManager.CalculateClipLength(audioClip.length, audioSource.pitch);
        ObjectDelays.Delay(() => Object.Destroy(sfxObject), destroyTime, sfx.useRealtimeToDestroy);
      }
    }
  }
}