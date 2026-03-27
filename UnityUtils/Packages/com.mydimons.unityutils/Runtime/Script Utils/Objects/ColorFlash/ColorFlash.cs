using UnityEngine;

[System.Serializable]
public class ColorFlash {
  /// <summary>
  /// The <see cref="Color"/> that is flashed to.
  /// </summary>
  [ColorUsage(true, true)]
  public Color color = Color.white;

  /// <summary>
  /// The amount of time, in seconds, that the flash is at <see cref="amount"/> for after fade in and before fade out.
  /// </summary>
  [Header("Duration and Timings")]
  public float duration = 0.1f;

  /// <summary>
  /// The animation curve that controls the fade in of the flash.  
  /// </summary>
  [Space(10)]
  public AnimationCurve fadeInCurve = AnimationCurve.Linear(0, 0, 1, 1);
  /// <summary>
  /// The amount of time, in seconds, that the flash takes to fade in from 0 intensity to <see cref="amount"/> at the start of the flash.
  /// </summary>
  public float fadeInTime = 0;

  /// <summary>
  /// The animation curve that controls the fade out of the flash.  
  /// </summary>
  [Space(10)]
  public AnimationCurve fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1);
  /// <summary>
  /// The amount of time, in seconds, that the flash takes to fade out from <see cref="amount"/> to 0 intensity at the end of the flash.
  /// </summary>
  public float fadeOutTime = 0;

  /// <summary>
  /// A 0 to 1 range of the intensity of the flash. 0 means no flash, and 1 means full flash.
  /// </summary>
  [Header("Other attributes")]
  [Range(0, 1)]
  public float amount = 1;

  /// <summary>
  /// if True, will use unscaled time instead of scaled time for the flash duration and fade in/out times.
  /// </summary>
  public bool useRealtime = false;
}
