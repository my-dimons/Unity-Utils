using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button), typeof(UIButtonDebug))]
  public class UIButtonHoverPosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    /// <summary>
    /// When hovered this is the size the button will be set to.
    /// </summary>
    [Header("Adjustable Values")]
    public Vector3 hoverLocalPosition;

    /// <summary>
    /// The amount of seconds that the button will size up or down in.
    /// </summary>
    public float sizeAnimationSeconds = 0.1f;

    [Space(8)]

    /// <summary>
    /// true to use unscaled real time for the animation (ignoring time scale)
    /// </summary>
    public bool useRealtime = true;

    /// <summary>
    /// The <see cref="AnimationCurve"/> that the button will follow.
    /// </summary>
    public AnimationCurve SizingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


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

    private Vector3 originalPosition;
    private Vector3 hoverPositionVector;
    private UIButtonDebug buttonDebug;

    // Starter is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
      originalPosition = transform.localPosition;

      buttonDebug = GetComponent<UIButtonDebug>();
    }

    // Update is called once per frame
    void Update() {
      hoverPositionVector = transform.localPosition + hoverLocalPosition;

      // Stops choppy animation when spam hovering the button
      if (!buttonDebug && transform.localPosition == hoverPositionVector) {
        ExitHoverAnimation();
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (transform.localPosition == originalPosition)
        EnterHoverAnimation();
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (transform.localPosition == hoverPositionVector)
        ExitHoverAnimation();
    }

    /// <summary>
    /// Moves the button to the set position: (<see cref="hoverPositionVector"/>).
    /// </summary>
    void EnterHoverAnimation() {
      ObjectAnimations.AnimateTransformPosition(applyTransform, originalPosition, hoverPositionVector, sizeAnimationSeconds, useRealtime, SizingCurve);

      if (debugLogs.logIn)
        Debug.Log("Moving button to position");

      LogMove();
    }

    /// <summary>
    /// Moves the button to its original position.
    /// </summary>
    void ExitHoverAnimation() {
      ObjectAnimations.AnimateTransformPosition(applyTransform, hoverPositionVector, originalPosition, sizeAnimationSeconds, useRealtime, SizingCurve);

      if (debugLogs.logOut)
        Debug.Log("Moving button back");

      LogMove();
    }

    private void LogMove() {
      if (debugLogs.logAny)
        Debug.Log("Moved button");
    }

    void Reset() {
      applyTransform = gameObject.transform;
    }
  }
}
