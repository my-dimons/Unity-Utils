using UnityEngine;
using System.Collections;

/*
 *
 * This script should be created in the main scene, and it will persist through all scenes
 * 
 * This script is designed to keep a set of predefined music tracks playing randomly in the background. 
 * It does not support dynamic soundtracks, such as changing music based on the player’s location or game events.
 * But this script does have a few functions to stop and start music
 * 
 * Sometimes this script will bug out if you don't have run in background enabled in Unity (https://discussions.unity.com/t/how-do-you-keep-your-game-running-even-when-you-switch-out-of-it/928)
 *
 */

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicTracks;
    [Space(4)]
    public AudioClip currentPlayingTrack;
    [Space(8)]
    [Tooltip("Immedietely start playing music when this object loads in")]
    public bool playOnAwake = true;

    public float fadeTime;
    [Tooltip("If you want a single value cooldown, just set the values to the same value")]
    public Vector2 randomMillisecondCooldownBetweenSongs;

    public static BackgroundMusicManager Instance { get; private set; }

    public Coroutine playingMusicCoroutine;

    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (playOnAwake)
            StartContinuousMusic();
            
    }

    private void Update()
    {
        if (musicSource != null)
            musicSource.volume = AudioManager.CalculateVolumeBasedOnType(1, AudioManager.AudioType.music);
    }

    /// <summary>
    /// Loops through random songs in <see cref="musicTracks"/> constantly
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayMusicContinuously()
    {
        while (true)
        {
            PlaySingleRandomMusicTrack();

            Debug.Log("Clip Started");
            yield return new WaitWhile(() => musicSource.isPlaying);
            Debug.Log("Clip Ended");

            Debug.Log("Finished Music Clip");

            float waitTimeUntillNextSong = Random.Range(randomMillisecondCooldownBetweenSongs.x, randomMillisecondCooldownBetweenSongs.y);
            yield return new WaitForSecondsRealtime(waitTimeUntillNextSong);
        }
    }

    /// <summary>
    /// Stops the <see cref="musicSource"/> from playing music and looping until <see cref="PlayMusicContinuously"/> is called again (to start looping)
    /// </summary>
    public void StopMusic()
    {
        if (Instance.musicSource.isPlaying)
        {
            Instance.musicSource.Stop();

            if (Instance.playingMusicCoroutine != null)
            {
                Instance.StopCoroutine(Instance.playingMusicCoroutine);
                Instance.playingMusicCoroutine = null;
            }
        }
    }

    /// <summary>
    /// Starts to <see cref="PlayMusicContinuously"/> until stopped
    /// </summary>
    public void StartContinuousMusic()
    {
        bool canPlayMusic = !Instance.musicSource.isPlaying && musicTracks.Length > 0;
        if (canPlayMusic)
            playingMusicCoroutine = StartCoroutine(PlayMusicContinuously());
        else if (musicTracks.Length > 0)
            Debug.LogWarning("No music tracks in MusicManager.cs");
        else if (!Instance.musicSource.isPlaying)
            Debug.LogWarning("Tried starting continuous music but music source is already playing!");
    }

    /// <summary>
    /// Plays a music track on the <see cref="musicSource"/>
    /// </summary>
    /// <param name="clip<see cref="AudioClip"/> too play"></param>
    private void PlayMusicTrack(AudioClip clip)
    {
        musicSource.clip = clip;
        currentPlayingTrack = clip;

        musicSource.Play();

        Debug.Log($"Playing: {clip.name}");
    }

    /// <summary>
    /// Plays a random music track once
    /// </summary>
    public void PlaySingleRandomMusicTrack()
    {
        PlayMusicTrack(GetRandomSong());
    }

    /// <summary>
    /// Plays a specific music track once
    /// </summary>
    /// <param name="clip"><see cref="AudioClip"/> to play</param>
    public void PlaySpecificMusicTrack(AudioClip clip)
    {
        Instance.PlayMusicTrack(clip);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Random music track within the <see cref="musicTracks"/> array</returns>
    private AudioClip GetRandomSong()
    {
        int randomSongTrackIndex = Random.Range(0, musicTracks.Length);
        return musicTracks[randomSongTrackIndex];
    }
}
