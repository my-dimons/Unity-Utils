using NUnit;
using System;
using System.Collections;
using UnityEngine;

/*
 *
 * This script should be created in the main scene, and it will persist through all scenes
 * 
 * This script is designed to keep a set of predefined Music tracks playing randomly in the background. 
 * It does not support dynamic soundtracks, such as changing Music based on the player's location or game events.
 * But this script does have a few functions to stop and start Music
 * 
 * Sometimes this script will bug out if you don't have run in background enabled in Unity (https://discussions.unity.com/t/how-do-you-keep-your-game-running-even-when-you-switch-out-of-it/928)
 *
 */

namespace UnityUtils.ScriptUtils.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusicManager : MonoBehaviour
    {
        private AudioSource musicSource;

        [Header("Music")]

        /// An array of audio clips representing the available Music tracks.
        public AudioClip[] musicTracks;

        /// The current playing track.
        public AudioClip currentPlayingTrack;

        [Header("Variables")]

        /// Whether to start playing Music as soon as this object awakes.
        public bool playOnAwake = true;

        /// The duration, in seconds, over which the fade in/out effect occurs.
        public float fadeTime;

        [Range(0, 1)]
        private float fadeVolume;

        /// Random cooldown time between songs in milliseconds
        public Vector2 randomSecondCooldownBetweenSongs;

        [Header("Debug")]

        /// If true, will output a Debug.Log when a new track starts playing.
        public bool logOnSongPlay;

        /// If true, will output a Debug.Log when a track stops playing.
        public bool logOnSongStop;

        /// If true, will output a Debug.Log the <see cref="randomSecondCooldownBetweenSongs"/> when calculated.
        public bool logRandomSongCooldown;

        /// If true, will output a Debug.Log every frame, detailing how much of the current song has been played.
        public bool logSongProgress;
        /// Will only log song progerss every logSongProgessEveryPercent percent.
        public float logSongProgessEveryPercent = 1;
        private float lastLoggedPercent;

        public static BackgroundMusicManager Instance { get; private set; }

        public Coroutine playingMusicCoroutine;

        void Awake()
        {
            musicSource = GetComponent<AudioSource>();

            if (Instance == null) Instance = this; else Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            if (fadeTime <= 0)
            {
                fadeVolume = 1;
            }
        }

        private void Start()
        {
            if (playOnAwake)
            {
                StopContinousMusic();
                PlayContinuousMusic();
            }
        }

        private void Update()
        {
            CalculateMusicVolume();

            if (playingMusicCoroutine != null && musicSource.isPlaying && logSongProgress)
            {
                DebugSongProgress();
            }
            //Debug.Log("Fade Volume: " + fadeVolume);
        }

        private void CalculateMusicVolume()
        {
            musicSource.volume = AudioManager.CalculateVolumeBasedOnType(1 * fadeVolume, AudioManager.VolumeType.Music);
        }

        /// <summary>
        /// Loops through random songs in <see cref="musicTracks"/> constantly
        /// </summary>
        private IEnumerator PlayMusicContinuously()
        {
            while (true)
            {
                PlaySingleRandomMusicTrack();

                TweenVolume(0, 1);

                yield return new WaitForSecondsRealtime(currentPlayingTrack.length - fadeTime);

                TweenVolume(1, 0);

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
        public void StopContinousMusic()
        {
            if (musicSource.isPlaying)
            {
                StopMusicSource();

                if (playingMusicCoroutine != null)
                {
                    StopCoroutine(playingMusicCoroutine);
                    playingMusicCoroutine = null;
                }
            }
        }

        public void StopMusicSource()
        {
            musicSource.Stop();
            currentPlayingTrack = null;

            lastLoggedPercent = 0;

            if (logOnSongStop)
                Debug.Log("Stopped playing music");
        }

        /// <summary>
        /// Starts to <see cref="PlayMusicContinuously"/> until stopped
        /// </summary>
        public void PlayContinuousMusic()
        {
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
        /// Plays a Music track on the <see cref="musicSource"/>
        /// </summary>
        private void PlayMusicTrack(AudioClip clip)
        {
            musicSource.clip = clip;
            currentPlayingTrack = clip;

            musicSource.Play();

            if (logOnSongPlay)
                Debug.Log($"Playing music track: {clip.name}");
        }

        /// <summary>
        /// Plays a random song in <see cref="musicTracks"/> once
        /// </summary>
        public void PlaySingleRandomMusicTrack()
        {
            PlayMusicTrack(GetRandomSong());
        }

        /// <summary>
        /// Plays a specific Music track once
        /// </summary>
        public void PlaySpecificMusicTrack(AudioClip clip)
        {
            Instance.PlayMusicTrack(clip);
        }

        /// <returns>Random Music track within <see cref="musicTracks"/></returns>
        public AudioClip GetRandomSong()
        {
            int randomSongTrackIndex = UnityEngine.Random.Range(0, musicTracks.Length);
            return musicTracks[randomSongTrackIndex];
        }

        private void TweenVolume(float start, float end)
        {
            ObjectAnimations.AnimateValue<float>(start, end, fadeTime, (a, b, t) => Mathf.Lerp(a, b, t), value => fadeVolume = value, true);
        }

        private void DebugSongProgress()
        {
            int decimalRounding = 2;
            float progressPercent = (musicSource.time / currentPlayingTrack.length) * 100;

            bool logPercent = progressPercent > lastLoggedPercent + logSongProgessEveryPercent;

            if (logPercent)
            {
                Debug.Log("Current song progress: "
                    + Math.Round(progressPercent, decimalRounding) + "% ("
                    + Math.Round(musicSource.time, decimalRounding) + "s / "
                    + Math.Round(currentPlayingTrack.length, decimalRounding) + "s)");

                lastLoggedPercent = progressPercent;
            }
        }
    }
}