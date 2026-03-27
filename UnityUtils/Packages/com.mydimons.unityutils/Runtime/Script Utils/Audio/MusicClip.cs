using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  [System.Serializable]
  public abstract class MusicClip {
    /// <summary>
    /// The music clip played.
    /// </summary>
    public AudioClip musicClip;
    /// <summary>
    /// The name of this music clip.
    /// </summary>
    public string name;

    [Header("Audio Settings")]
    /// <summary>
    /// The volume this music clip gets played at 
    /// </summary>
    [Range(0, 1)]
    public float volume = 1;
    /// <summary>
    /// The pitch this music clip gets played at
    /// </summary>
    public float pitch = 1;

    /// <returns>If true, the clip can be played when picking music</returns>
    public abstract bool CanBePlayed();
  }
}