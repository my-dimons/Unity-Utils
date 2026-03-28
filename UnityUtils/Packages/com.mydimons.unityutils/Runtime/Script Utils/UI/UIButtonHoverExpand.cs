using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button), typeof(UIButtonDebug))]
  public class UIButtonHoverExpand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    /// <summary>
    /// When hovered this is the size the button will be set to.
    /// </summary>
    [Header("Adjustable Values")]
    public float hoverSize = 1.1f;

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

    private Vector3 originalSize;
    private Vector3 hoverSizeVector;
    private UIButtonDebug buttonDebug;

    // Starter is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
      originalSize = transform.localScale;

      buttonDebug = GetComponent<UIButtonDebug>();
    }

    // Update is called once per frame
    void Update() {
      hoverSizeVector = new Vector3(hoverSize, hoverSize, hoverSize);

      // Stops choppy animation when spam hovering the button
      if (!buttonDebug && transform.localScale == hoverSizeVector) {
        ExitHoverAnimation();
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      if (transform.localScale == originalSize)
        EnterHoverAnimation();
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (transform.localScale == hoverSizeVector)
        ExitHoverAnimation();
    }

    /// <summary>
    /// Grows the button to the set size: (<see cref="hoverSizeVector"/>).
    /// </summary>
    void EnterHoverAnimation() {
      ObjectAnimations.AnimateTransformScale(applyTransform, originalSize, hoverSizeVector, sizeAnimationSeconds, useRealtime, SizingCurve);

      if (debugLogs.logIn)
        Debug.Log("Scaling button up");

      DebugRotate();
    }

    /// <summary>
    /// Shrinks the button to its original size.
    /// </summary>
    void ExitHoverAnimation() {
      ObjectAnimations.AnimateTransformScale(applyTransform, hoverSizeVector, originalSize, sizeAnimationSeconds, useRealtime, SizingCurve);

      if (debugLogs.logOut)
        Debug.Log("Scaling button down");

      DebugRotate();
    }

    private void DebugRotate() {
      if (debugLogs.logAny)
        Debug.Log("Scaled button");
    }

    void Reset() {
      applyTransform = gameObject.transform;
    }
  }
}
