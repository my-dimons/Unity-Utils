using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button), typeof(UIButtonDebug))]
  public class UIButtonHoverRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    /// <summary>
    /// When hovered this is the rotation the button will be set to.
    /// </summary>
    [Header("Adjustable Values")]
    public float hoverRotation = 8f;

    /// <summary>
    /// The amount of seconds that the button will rotate in.
    /// </summary>
    public float rotationAnimationSeconds = 0.1f;

    /// <summary>
    /// If true the button will rotate to the set position, the rotate back on hover. If false the buton will rotate to the set position, then only rotate back when unhovering.
    /// </summary>
    public bool rotateBackAfterHover = true;

    [Space(8)]

    /// <summary>
    /// true to use unscaled real time for the animation (ignoring time scale).
    /// </summary>
    public bool useRealtime = true;

    /// <summary>
    /// The <see cref="AnimationCurve"/> that the button will follow.
    /// </summary>
    public AnimationCurve SizingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    /// <summary>
    /// Not required, but if true rotation will use random rotation.
    /// </summary>
    [Header("Random Rotation")]
    public bool useRandomRotation = false;

    /// <summary>
    /// The min/max values to use in random rotation
    /// </summary>
    public Vector2 randomRotation = new Vector2(-5f, 5f);
    private Vector3 currentRandomRotation;


    /// <summary>
    /// The object to apply the transform to (Default's to the applied object).
    /// </summary>
    [Header("Applied Transform")]
    public Transform applyTransform;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> depending on the <see cref="UIButtonDebugSettings"/>
    /// </summary>
    [Header("Debug.Logs()")]
    public UIButtonDebugSettings debugLogs;
    /// <summary>
    /// If <see cref="useRandomRotation"/> is true, will output a <see cref="Debug.Log(object)"/> when the random pos is generated.
    /// </summary>
    public bool logRandomRotation;

    private Vector3 originalRotation;
    private Vector3 hoverRotationVector;
    private UIButtonDebug buttonDebug;

    // Starter is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
      originalRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);

      buttonDebug = GetComponent<UIButtonDebug>();
    }

    // Update is called once per frame
    void Update() {
      hoverRotationVector = useRandomRotation ? currentRandomRotation : new Vector3(0, 0, hoverRotation);

      // Stops choppy animation when spam hovering the button
      bool stopChoppyAnimation = !buttonDebug.HoveringOverButton && transform.localRotation == Quaternion.Euler(hoverRotationVector);
      bool rotateBackAfterHoverCondition = transform.localRotation == Quaternion.Euler(hoverRotationVector) && rotateBackAfterHover;

      if (stopChoppyAnimation || rotateBackAfterHoverCondition) {
        ExitHoverAnimation();
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (transform.localRotation == Quaternion.Euler(originalRotation))
        EnterHoverAnimation();
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (transform.localRotation == Quaternion.Euler(hoverRotationVector))
        ExitHoverAnimation();
    }

    /// <summary>
    /// Grows the button to the original size (<see cref="hoverRotationVector"/>).
    /// </summary>
    void EnterHoverAnimation() {
      currentRandomRotation = new Vector3(originalRotation.x, originalRotation.y, Random.Range(randomRotation.x, randomRotation.y));

      ObjectAnimations.AnimateTransformRotation(applyTransform, originalRotation, useRandomRotation ? currentRandomRotation : hoverRotationVector, rotationAnimationSeconds, useRealtime, SizingCurve);

      if (debugLogs.logIn)
        Debug.Log("Rotating button to set rotation");
      if (logRandomRotation && useRandomRotation)
        Debug.Log("Generated random rotation: " + currentRandomRotation);

      LogRotate();
    }

    /// <summary>
    /// Shrinks the button to its original size.
    /// </summary>
    void ExitHoverAnimation() {
      ObjectAnimations.AnimateTransformRotation(applyTransform, useRandomRotation ? currentRandomRotation : hoverRotationVector, originalRotation, rotationAnimationSeconds, useRealtime, SizingCurve);

      if (debugLogs.logOut)
        Debug.Log("Rotating button back");

      LogRotate();
    }

    private void LogRotate() {
      if (debugLogs.logAny)
        Debug.Log("Rotated button");
    }

    void Reset() {
      applyTransform = gameObject.transform;
    }
  }
}