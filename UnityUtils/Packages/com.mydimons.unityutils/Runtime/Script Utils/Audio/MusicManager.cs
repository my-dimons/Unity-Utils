using System;
using System.Collections;
using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.Audio {
  [RequireComponent(typeof(AudioSource))]
  public class MusicManager : MonoBehaviour {
    private AudioSource musicSource;
    public static MusicManager Instance { get; private set; }

    /// <summary>
    /// An array of audio clips representing the available Music tracks.
    /// </summary>
    [Header("Music")]
    public MusicClip[] musicTracks;

    /// <summary>
    /// The current playing track.
    /// </summary>
    public MusicClip currentPlayingTrack;

    /// <summary>
    /// Whether to start playing Music as soon as this object awakes.
    /// </summary>
    [Header("Variables")]
    public bool playOnAwake = true;

    /// <summary>
    /// The duration, in seconds, over which the fade in/out effect occurs.
    /// </summary>
    public float fadeTime;

    [Range(0, 1)]
    private float fadeVolume;

    /// <summary>
    /// Random cooldown time between songs in milliseconds
    /// </summary>
    public Vector2 randomSecondCooldownBetweenSongs;

    /// <summary>
    /// If true, will output a <see cref="Debug.Log(object)"/> when a new track starts playing.
    /// </summary>
    [Header("Debug")]
    public bool logOnPlayMusicTrack;

    /// <summary>
    /// If true, will output a <see cref="Debug.Log(object)"/> when a track stops playing.
    /// </summary>
    public bool logOnStopMusicTrack;

    /// <summary>
    /// If true, will output a <see cref="Debug.Log(object)"/> the <see cref="randomSecondCooldownBetweenSongs"/> when calculated.
    /// </summary>
    public bool logRandomSongCooldown;

    /// <summary>
    /// If true, will output a <see cref="Debug.Log(object)"/> when the fade time is not 0 or 1 (When its animating).
    /// </summary>
    public bool logFadeVolume;

    /// <summary>
    /// If true, will output a <see cref="Debug.Log(object)"/> every <see cref="logSongProgessEveryPercent"/>, detailing how much of the current song has been played.
    /// </summary>
    public bool logSongProgress;

    /// <summary>
    /// Will only log song progress every <see cref="logSongProgessEveryPercent"/> PERCENT.
    /// </summary>
    public float logSongProgessEveryPercent = 1;

    private float lastLoggedPercent;

    /// <summary>
    /// The <see cref="Coroutine"/> playing music. Is able to be canceled.
    /// </summary>
    public Coroutine playingMusicCoroutine;

    /// <summary>
    /// Called when a music track starts playing, with the track that started playing as a parameter.
    /// </summary>
    public static Action<MusicClip> OnPlayMusicTrack;
    /// <summary>
    /// Called when a music track stops playing, with the track that stopped playing as a parameter.
    /// </summary>
    public static Action<MusicClip> OnStopMusicTrack;

    void Awake() {
      musicSource = GetComponent<AudioSource>();

      if (Instance == null)
        Instance = this;
      else
        Destroy(gameObject);
      DontDestroyOnLoad(gameObject);

      if (fadeTime <= 0) {
        fadeVolume = AudioManager.MAX_AUDIO_VOLUME;
      }
    }

    private void Start() {
      if (playOnAwake) {
        StopContinousMusic();
        PlayContinuousMusic();
      }
    }

    private void Update() {
      musicSource.volume = AudioManager.CalculateVolumeBasedOnType(AudioManager.MAX_AUDIO_VOLUME * fadeVolume, AudioManager.VolumeType.Music);

      #region Debug.Logs()
      // Song progress
      if (playingMusicCoroutine != null && musicSource.isPlaying && logSongProgress) {
        DebugLogSongProgress();
      }

      // Fade volume
      bool fadeVolumeInRange = fadeVolume > 0 && fadeVolume < 1;
      if (logFadeVolume && fadeVolumeInRange) {
        Debug.Log("Fade volume: " + fadeVolume);
      }
      #endregion
    }


    /// <summary>
    /// Starts to <see cref="PlayMusicContinuously"/> until stopped
    /// </summary>
    public void PlayContinuousMusic() {
      bool enoughMusicTracks = musicTracks.Length > 0;
      bool musicAlreadyPlaying = !musicSource.isPlaying;

      if (musicAlreadyPlaying && enoughMusicTracks)
        playingMusicCoroutine = StartCoroutine(PlayMusicContinuously());

      else if (!enoughMusicTracks)
        Debug.LogWarning("No music tracks found!");
      else if (musicAlreadyPlaying)
        Debug.LogWarning("Tried starting continuous music but music source is already playing!");
    }


    /// <summary>
    /// Loops through random songs in <see cref="musicTracks"/> constantly
    /// </summary>
    private IEnumerator PlayMusicContinuously() {
      while (true) {
        PlayRandomMusicTrack();

        TweenVolume(AudioManager.MIN_AUDIO_VOLUME, AudioManager.MAX_AUDIO_VOLUME);

        yield return new WaitForSecondsRealtime(currentPlayingTrack.musicClip.length - fadeTime);

        TweenVolume(AudioManager.MAX_AUDIO_VOLUME, AudioManager.MIN_AUDIO_VOLUME);

        yield return new WaitForSecondsRealtime(fadeTime);

        StopMusicSource();

        float waitTimeUntilNextSong = UnityEngine.Random.Range(randomSecondCooldownBetweenSongs.x, randomSecondCooldownBetweenSongs.y);
        if (logRandomSongCooldown)
          Debug.Log("Time until next song: " + waitTimeUntilNextSong + "s");
        yield return new WaitForSecondsRealtime(waitTimeUntilNextSong);
      }
    }

    /// <summary>
    /// Stops the <see cref="musicSource"/> from playing Music and looping until <see cref="PlayMusicContinuously"/> is called again (to start looping)
    /// </summary>
    public void StopContinousMusic() {
      if (IsPlayingMusic()) {
        StopMusicSource();

        if (playingMusicCoroutine != null) {
          StopCoroutine(playingMusicCoroutine);
          playingMusicCoroutine = null;
        }
      }
    }

    private void StopMusicSource() {
      OnStopMusicTrack?.Invoke(currentPlayingTrack);
      musicSource.Stop();
      currentPlayingTrack = null;

      lastLoggedPercent = 0;
    }


    /// <summary>
    /// Plays a Music track on the <see cref="musicSource"/>
    /// </summary>
    private void PlayMusicTrack(MusicClip clip) {
      musicSource.clip = clip.musicClip;
      musicSource.pitch = AudioManager.CalculatePitchVariance(clip.pitchSettings.pitchVariance, clip.pitchSettings.pitch);
      musicSource.volume = AudioManager.CalculateVolumeBasedOnType(1, clip.volumeSettings.volumeType);
      currentPlayingTrack = clip;

      OnPlayMusicTrack?.Invoke(currentPlayingTrack);
      musicSource.Play();

      if (logOnPlayMusicTrack)
        Debug.Log($"Playing music track: {clip.name}");
    }

    /// <summary>
    /// Plays a random song in <see cref="musicTracks"/> once
    /// </summary>
    public void PlayRandomMusicTrack() {
      PlayMusicTrack(GetRandomSong());
    }

    /// <summary>
    /// Plays a specific Music track once
    /// </summary>
    public void PlaySpecificMusicTrack(MusicClip clip) {
      Instance.PlayMusicTrack(clip);
    }

    /// <returns>Random Music track within <see cref="musicTracks"/></returns>
    public MusicClip GetRandomSong() {
      int randomSongTrackIndex = UnityEngine.Random.Range(0, musicTracks.Length);
      return musicTracks[randomSongTrackIndex];
    }

    /// <summary>
    /// Returns whether music is currently playing or not.
    /// </summary>
    public bool IsPlayingMusic() {
      return musicSource.isPlaying;
    }

    private void TweenVolume(float start, float end) {
      ObjectAnimations.AnimateValue<float>(start, end, fadeTime, (a, b, t) => Mathf.Lerp(a, b, t), value => fadeVolume = value, true);
    }

    private void DebugLogSongProgress() {
      const int DECIMAL_ROUNDING = 2;
      const int PERCENT = 100;

      float progressPercent = (musicSource.time / currentPlayingTrack.musicClip.length) * PERCENT;

      bool logPercent = progressPercent > lastLoggedPercent + logSongProgessEveryPercent;

      if (logPercent) {
        Debug.Log("Current song progress: "
            + Math.Round(progressPercent, DECIMAL_ROUNDING) + "% ("
            + Math.Round(musicSource.time, DECIMAL_ROUNDING) + "s / "
            + Math.Round(currentPlayingTrack.musicClip.length, DECIMAL_ROUNDING) + "s)");

        lastLoggedPercent = progressPercent;
      }
    }

    private void DebugLogOnPlayMusicTrack(MusicClip clip) {
      if (logOnPlayMusicTrack)
        Debug.Log($"Started playing music track: {clip}");
    }

    private void DebugLogOnStopMusicTrack(MusicClip clip) {
      if (logOnStopMusicTrack) {
        Debug.Log($"Stopped playing music track: {clip}");
      }
    }

    private void OnEnable() {
      OnPlayMusicTrack += DebugLogOnPlayMusicTrack;

      OnStopMusicTrack += DebugLogOnStopMusicTrack;
    }

    private void OnDisable() {
      OnPlayMusicTrack -= DebugLogOnPlayMusicTrack;

      OnStopMusicTrack -= DebugLogOnStopMusicTrack;
    }
  }
}