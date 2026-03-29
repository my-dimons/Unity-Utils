using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  [CreateAssetMenu(fileName = "BackgroundMusic", menuName = "UnityUtils/Audio/Background Music", order = 0)]
  public class BackgroundMusic : MusicClip {
    /// <summary>
    /// Will always return true, as background music should always be able to be played when picked by the <see cref="MusicManager"/>.
    /// <returns></returns>
    public override bool CanBePlayed() {
      // Add logic to decide if this clip can be played
      return true;
    }
  }
}