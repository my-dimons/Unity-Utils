using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  [CreateAssetMenu(fileName = "BackgroundMusic", menuName = "UnityUtils/Audio/Background Music", order = 0)]
  public class BackgroundMusic : MusicClip {
    public override bool CanBePlayed() {
      // Add logic to decide if this clip can be played
      return true;
    }
  }
}