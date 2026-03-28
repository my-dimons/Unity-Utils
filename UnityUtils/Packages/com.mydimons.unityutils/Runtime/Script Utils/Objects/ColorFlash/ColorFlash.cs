using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects.ColorFlash {
  [System.Serializable]
  public class ColorFlash {
    /// <summary>
    /// Creates a default color flash with the following properties:
    /// <para>- <see cref="color"/>: <see cref="Color.white"/></para>
    /// <para>- <see cref="durationSeconds"/>: 0.1s</para>
    /// <para>- <see cref="fadeInCurve"/>: <see cref="AnimationCurve.Linear(float, float, float, float)"/> from 0 to 1</para>
    /// <para>- <see cref="fadeInTimeSeconds"/>: 0s</para>
    /// <para>- <see cref="fadeOutCurve"/>: <see cref="AnimationCurve.Linear(float, float, float, float)"/> from 0 to 1</para>
    /// <para>- <see cref="fadeOutTimeSeconds"/>: 0s</para>
    /// <para>- <see cref="flashAmount"/>: 1</para>
    /// <para>- <see cref="useRealtime"/>: false</para>
    /// </summary>
    public static ColorFlash CreateDefaultFlash() {
      return new ColorFlash {
        color = Color.white,
        durationSeconds = 0.1f,
        fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1),
        fadeInTimeSeconds = 0,
        fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1),
        fadeOutTimeSeconds = 0,
        flashAmount = 1
      };
    }

    /// <summary>
    /// Creates a default color flash with the following properties:
    /// <para>- <see cref="color"/>: <see cref="Color.white"/></para>
    /// <para>- <see cref="durationSeconds"/>: 0s</para>
    /// <para>- <see cref="fadeInCurve"/>: <see cref="AnimationCurve.Linear(float, float, float, float)"/> from 0 to 1</para>
    /// <para>- <see cref="fadeInTimeSeconds"/>: 0s</para>
    /// <para>- <see cref="fadeOutCurve"/>: <see cref="AnimationCurve.Linear(float, float, float, float)"/> from 0 to 1</para>
    /// <para>- <see cref="fadeOutTimeSeconds"/>: 0.25s</para>
    /// <para>- <see cref="flashAmount"/>: 1</para>
    /// <para>- <see cref="useRealtime"/>: false</para>
    /// </summary>
    public static ColorFlash CreateDefaultFadeOutFlash() {
      return new ColorFlash {
        color = Color.white,
        durationSeconds = 0,
        fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1),
        fadeInTimeSeconds = 0,
        fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1),
        fadeOutTimeSeconds = 0.25f,
        flashAmount = 1
      };
    }

    /// <summary>
    /// The <see cref="Color"/> that is flashed to.
    /// </summary>
    [ColorUsage(true, true)]
    public Color color = Color.white;

    /// <summary>
    /// The amount of time, in seconds, that the flash is at <see cref="flashAmount"/> for after fade in and before fade out.
    /// </summary>
    [Header("Duration and Timings")]
    public float durationSeconds = 0.1f;

    /// <summary>
    /// The animation curve that controls the fade in of the flash.  
    /// </summary>
    [Space(10)]
    public AnimationCurve fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1);
    /// <summary>
    /// The amount of time, in seconds, that the flash takes to fade in from 0 intensity to <see cref="flashAmount"/> at the start of the flash.
    /// </summary>
    public float fadeInTimeSeconds = 0;

    /// <summary>
    /// The animation curve that controls the fade out of the flash.  
    /// </summary>
    [Space(10)]
    public AnimationCurve fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1);
    /// <summary>
    /// The amount of time, in seconds, that the flash takes to fade out from <see cref="flashAmount"/> to 0 intensity at the end of the flash.
    /// </summary>
    public float fadeOutTimeSeconds = 0;

    /// <summary>
    /// A 0 to 1 range of the intensity of the flash. 0 means no flash, and 1 means full flash.
    /// </summary>
    [Header("Other attributes")]
    [Range(0, 1)]
    public float flashAmount = 1;

    /// <summary>
    /// if True, will use unscaled time instead of scaled time for the flash duration and fade in/out times.
    /// </summary>
    public bool useRealtime = false;

    /// <summary>
    /// Sets the <see cref="fadeInTimeSeconds"/> and <see cref="fadeInCurve"/> properties
    /// </summary>
    public ColorFlash SetFadeIn(float fadeInTime, AnimationCurve fadeInCurve) {
      this.fadeInTimeSeconds = fadeInTime;
      this.fadeInCurve = fadeInCurve;
      return this;
    }

    /// <summary>
    /// Sets the <see cref="fadeOutTimeSeconds"/> and <see cref="fadeOutCurve"/> properties
    /// </summary>
    public ColorFlash SetFadeOut(float fadeOutTime, AnimationCurve fadeOutCurve) {
      this.fadeOutTimeSeconds = fadeOutTime;
      this.fadeOutCurve = fadeOutCurve;
      return this;
    }
  }
}