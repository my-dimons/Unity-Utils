using UnityEngine;
using UnityUtils.ScriptUtils.Audio;
using UnityUtils.ScriptUtils.Objects;

public class TestingScript : MonoBehaviour {
  public AnimationCurve curve;
  public float testingValue;
  public Vector3 testingVector3;

  public SFX sfx = SFX.Create2DSFX();
  AudioClip clip;
  // Starter is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    ObjectAnimations.AnimateTransformRotation(transform, new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z), new Vector3(4, 50, 90), 2);
    ObjectAnimations.AnimateTransformScale(transform, transform.localScale, new Vector3(3, 4, 5), 2);
    ObjectAnimations.AnimateTransformPosition(transform, transform.position, new Vector3(0, 5, 0), 2, animationCurve: curve);

    SFX test = SFX.Create2DSFX().SetPitch(3, 4);
  }
}