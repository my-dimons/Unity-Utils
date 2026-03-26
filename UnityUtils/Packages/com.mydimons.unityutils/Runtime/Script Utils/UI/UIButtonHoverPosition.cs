using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
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
    /// True if the button is being hovered
    /// </summary>
    [Header("Debug Values")]
    public bool hoveringOverButton;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> on any moving.
    /// </summary>
    [Header("Debug Logs")]
    public bool logMove;
    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when moving on hover enter.
    /// </summary>
    public bool logEnterMove;
    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when moving on hover exit.
    /// </summary>
    public bool logExitMove;

    Vector3 originalPosition;
    Vector3 hoverPositionVector;

    // Starter is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
      originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
      hoverPositionVector = transform.localPosition + hoverLocalPosition;

      // Stops choppy animation when spam hovering the button
      if (!hoveringOverButton && transform.localPosition == hoverPositionVector) {
        ExitHoverAnimation();
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (transform.localPosition == originalPosition)
        EnterHoverAnimation();

      hoveringOverButton = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (transform.localPosition == hoverPositionVector)
        ExitHoverAnimation();

      hoveringOverButton = false;
    }

    /// <summary>
    /// Moves the button to the set position: (<see cref="hoverPositionVector"/>).
    /// </summary>
    void EnterHoverAnimation() {
      ObjectAnimations.AnimateTransformPosition(applyTransform, originalPosition, hoverPositionVector, sizeAnimationSeconds, useRealtime, SizingCurve);

      if (logEnterMove)
        Debug.Log("Moving button to position");

      LogMove();
    }

    /// <summary>
    /// Moves the button to its original position.
    /// </summary>
    void ExitHoverAnimation() {
      ObjectAnimations.AnimateTransformPosition(applyTransform, hoverPositionVector, originalPosition, sizeAnimationSeconds, useRealtime, SizingCurve);

      if (logExitMove)
        Debug.Log("Moving button back");

      LogMove();
    }

    private void LogMove() {
      if (logMove)
        Debug.Log("Moved button");
    }

    void Reset() {
      applyTransform = gameObject.transform;
    }
  }
}
