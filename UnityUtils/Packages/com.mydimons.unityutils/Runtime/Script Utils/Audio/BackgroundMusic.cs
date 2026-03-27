using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  [CreateAssetMenu(fileName = "BackgroundMusic", menuName = "UnityUtils/Audio/Background Music", order = 0)]
  public class BackgroundMusic : MusicClip {
    public override bool CanBePlayed() {
      return true;
    }
  }
}