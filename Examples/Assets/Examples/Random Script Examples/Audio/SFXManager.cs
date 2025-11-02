using UnityEngine;
using UnityEngine.Audio;

/*
 * To setup this script:
 *    - Create an empty game object in your scene and attach this script to it
 *    - Make sure you have a AudioManager.cs script in your project (This is used to get volume levels)
 * 
 *  To play audio globally (same volume no matter what), use "PlaySfxAudioClip()"
 *  To play audio spacialy (change volume and channel depending on location), use "PlaySpacialSfxAudioClip()"
 *  To play audio on a set audio audioSource (such as a character or object), use "PlayClipOnSource()"
 */

public class SFXManager : MonoBehaviour
{
    private static readonly float DEFAULT_PITCH_VARIANCE = 0.1f;

    public static SFXManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Plays an audio audioClip globaly (Not using spacial audio), at a set volume with a pitch variance (to feel less repetative)
    /// </summary>
    /// <param name="clip">Audio audioClip to play</param>
    /// <param name="volume">Volume to play audio audioClip at</param>
    /// <param name="pitchVariance">
    /// Random variance to make sound feel less repetative. 
    /// adds/subtracts a random pitch to the set audio clips pitch within the range of -pitchVariance, and pitchVariance 
    /// </param>
    public static void PlaySfxAudioClip(AudioClip clip, float volume = 1, float pitchVariance = default)
    {
        if (pitchVariance == default) pitchVariance = DEFAULT_PITCH_VARIANCE;

        CreateAndPlayAudioClip(clip, volume, pitchVariance, type: AudioManager.AudioType.sfx);
    }

    /// <summary>
    /// Plays an audio audioClip spacialy (Changes volume and L/R volume channel based on location) at a certain volume with a pitch variance (to feel less repetative)
    /// </summary>
    /// <param name="clip">Audio audioClip to play</param>
    /// <param name="position">Position to play audio audioClip at</param>
    /// <param name="volume">Volume to play audio audioClip at</param>
    /// <param name="pitchVariance">
    /// Random variance to make sound feel less repetative. 
    /// adds/subtracts a random pitch to the set audio clips pitch within the range of -pitchVariance, and pitchVariance 
    /// </param>
    public static void PlaySpacialSfxAudioClip(AudioClip clip, Vector3 position, float volume = 1, float pitchVariance = default)
    {
        if (pitchVariance == default) pitchVariance = DEFAULT_PITCH_VARIANCE;

        CreateAndPlayAudioClip(clip, volume, pitchVariance, position, type: AudioManager.AudioType.sfx);
    }

    /// <summary>
    /// Plays an AudioClip on an already existing AudioSource at a certain volume with a pitch variance (to feel less repetative)
    /// </summary>
    /// <param name="clip">Audio audioClip to play</param>
    /// <param name="source">Audio audioSource to play audioClip on</param>
    /// <param name="volume">Volume to play audio audioClip at</param>
    /// Random variance to make sound feel less repetative. 
    /// adds/subtracts a random pitch to the set audio clips pitch within the range of -pitchVariance, and pitchVariance 
    /// <param name="audioType">used to get the proper volume (does multiplication to set volume levels)</param>
    public static void PlayClipOnSource(AudioClip clip, AudioSource source, float volume = 1, float pitchVariance = default, AudioManager.AudioType audioType = default)
    {
        if (pitchVariance == default) pitchVariance = DEFAULT_PITCH_VARIANCE;
        if (audioType == default) audioType = AudioManager.AudioType.sfx;

        PlayAudioClipOnSource(clip, source, volume, pitchVariance, audioType);
    }

    private static void CreateAndPlayAudioClip(AudioClip clip, float volume, float pitchVariance, Vector3 position = default, Transform parent = default, AudioManager.AudioType type = default)
    {
        if (type == default) type = AudioManager.AudioType.sfx;

        GameObject temporaryGameObject = new GameObject("Audio Clip (Temporary)");
        AudioSource audioSource = temporaryGameObject.AddComponent<AudioSource>();

        Transform temporaryGameObjectParent = temporaryGameObject.transform.parent;
        if (parent == default)
            temporaryGameObjectParent = null;
        else
            temporaryGameObjectParent = parent;

        // set to global or spacial audio
        if (position == default)
        {
            temporaryGameObject.transform.position = Camera.main.transform.position;
            audioSource.spatialBlend = 0f; // 2D sound
        }
        else
        {
            temporaryGameObject.transform.position = position;
            audioSource.spatialBlend = 1f; // 3D sound
        }

        PlayAudioClipOnSource(clip, audioSource, volume, pitchVariance, type);

        float destroyTime = clip.length / audioSource.pitch;
        Destroy(temporaryGameObject, destroyTime);
    }

    private static void PlayAudioClipOnSource(AudioClip audioClip, AudioSource audioSource, float volume, float pitchVariance, AudioManager.AudioType audioType)
    {
        audioSource.clip = audioClip;
        audioSource.volume = AudioManager.GetVolumeBasedOnType(volume, audioType);

        float randomPitch = Random.Range(1 - pitchVariance, 1 + pitchVariance);
        audioSource.pitch = randomPitch;

        audioSource.Play();
    }
}
