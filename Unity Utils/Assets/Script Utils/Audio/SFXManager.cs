using UnityEngine;

/*
 * To setup this script:
 *    - Create an empty game object in your scene and attach this script to it
 *    - Make sure you have a AudioManager.cs script in your project (This is used to get volume levels)
 * 
 *  To play audio globally (same volume no matter what), use "PlaySfxAudioClip()"
 *  To play audio spacialy (change volume and channel depending on location), use "PlaySpacialSfxAudioClip()"
 *  To play audio on a set audio audioSource (such as a character or object), use "PlayClipOnSource()"
 */

public class SfxManager : MonoBehaviour
{
    [Tooltip("If true, this object will persist through all scenes")]
    public bool dontDestroyOnLoad = true;

    private const float DEFAULT_PITCH_VARIANCE = 0.1f;

    public static SfxManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Plays an <see cref="AudioClip"/> globally (non-spacialy) at a certain volume with a pitch variance (to feel less repetative)
    /// </summary>
    /// <param name="clip"><see cref="AudioClip"/> to play</param>
    /// <param name="volume">Playback volume</param>
    /// <param name="pitchVariance">
    /// Random variance to make sound feel less repetative. 
    /// randomly modifies the pitch of the music<see cref="AudioSource"/> in a random of 1 - pitchVariance, and 1 + pitchVariance (<see cref="DEFAULT_PITCH_VARIANCE"/> is the default value)
    /// </param>
    public void PlaySfxAudioClip(AudioClip clip, float volume = 1, float pitchVariance = default)
    {
        if (pitchVariance == default) pitchVariance = DEFAULT_PITCH_VARIANCE;

        CreateAndPlayAudioClip(clip, volume, pitchVariance, type: AudioManager.AudioType.sfx);
    }

    /// <summary>
    /// Plays an <see cref="AudioClip"/> globally (non-spacialy) at a certain volume for a set amount of time
    /// </summary>
    /// <param name="clip"><see cref="AudioClip"/> to play</param>
    /// <param name="time">Specified time for <see cref="AudioClip"/> to play for in seconds</param>
    /// <param name="volume">Playback volume</param>
    public void PlayTimedSFXAudioClip(AudioClip clip, float time, float volume = 1)
    {
        float clipLength = AudioManager.CalculateClipPitchWithLength(clip.length, time);

        CreateAndPlayAudioClip(clip, volume, pitch: clipLength, type: AudioManager.AudioType.sfx);
    }

    /// <summary>
    /// Plays an <see cref="AudioClip"/> spacially (Changes volume and L/R volume channel based on location) at a certain volume with a pitch variance (to feel less repetative)
    /// </summary>
    /// <param name="clip"><see cref="AudioClip"/> to play</param>
    /// <param name="position">Position to play <see cref="AudioClip"/> at</param>
    /// <param name="volume">Playback volume</param>
    /// <param name="pitchVariance">
    /// Random variance to make sound feel less repetative. 
    /// randomly modifies the pitch of the music<see cref="AudioSource"/> in a random of 1 - pitchVariance, and 1 + pitchVariance (<see cref="DEFAULT_PITCH_VARIANCE"/> is the default value)
    /// </param>
    public void PlaySpacialSfxAudioClip(AudioClip clip, Vector3 position, float volume = 1, float pitchVariance = default)
    {
        if (pitchVariance == default) pitchVariance = DEFAULT_PITCH_VARIANCE;

        CreateAndPlayAudioClip(clip, volume, pitchVariance, position: position, type: AudioManager.AudioType.sfx);
    }

    /// <summary>
    /// Plays an <see cref="AudioClip"/> on an already existing <see cref="AudioSource"/> at a certain volume with a pitch variance (to feel less repetative)
    /// </summary>
    /// <param name="volume">Playback volume</param>
    /// <param name="audioType">used to get the proper volume, see <see cref="AudioManager.CalculateVolumeBasedOnType(float, AudioManager.AudioType)"/> to get more info</param>
    /// <param name="pitchVariance"> 
    /// Random variance to make sound feel less repetative. 
    /// randomly modifies the pitch of the music<see cref="AudioSource"/> in a random of 1 - pitchVariance, and 1 + pitchVariance (<see cref="DEFAULT_PITCH_VARIANCE"/> is the default value)
    /// </param>
    public void PlayClipOnSource(AudioClip clip, AudioSource source, float volume = 1, float pitchVariance = default, AudioManager.AudioType audioType = AudioManager.AudioType.sfx)
    {
        if (pitchVariance == default) pitchVariance = DEFAULT_PITCH_VARIANCE;

        PlayAudioClipOnSource(clip, source, volume, pitchVariance, audioType);
    }

    private void CreateAndPlayAudioClip(AudioClip clip, float volume = 1, float pitchVariance = 0, float pitch = default, Vector3 position = default, Transform parent = default, AudioManager.AudioType type = AudioManager.AudioType.sfx)
    {
        if (type == default) type = AudioManager.AudioType.sfx;

        GameObject temporaryGameObject = new GameObject("Audio Clip (Temporary)");
        AudioSource audioSource = temporaryGameObject.AddComponent<AudioSource>();

        if (parent == default)
            temporaryGameObject.transform.parent = null;
        else
            temporaryGameObject.transform.parent = parent;

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

        if (pitch == default)
            PlayAudioClipOnSource(clip, audioSource, volume, pitchVariance, type);
        else
            PlayAudioClipOnSource(clip, audioSource, volume, 0f, type, pitch);

        float destroyTime = AudioManager.CalculateClipLength(clip.length, audioSource.pitch);
        Destroy(temporaryGameObject, destroyTime);
    }

    private void PlayAudioClipOnSource(AudioClip audioClip, AudioSource audioSource, float volume, float pitchVariance = default, AudioManager.AudioType audioType = AudioManager.AudioType.sfx, float pitch = default)
    {
        audioSource.clip = audioClip;
        audioSource.volume = AudioManager.CalculateVolumeBasedOnType(volume, audioType);

        float randomPitch = AudioManager.CalculatePitchVariance(pitchVariance);
        float usedPitch = pitch == default ? randomPitch : pitch;
        audioSource.pitch = usedPitch;

        audioSource.Play();
    }
}