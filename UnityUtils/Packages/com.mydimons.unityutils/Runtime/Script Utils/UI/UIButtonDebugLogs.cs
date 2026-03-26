using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonDebugLogs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when the button is hovered over.
    /// </summary>
    [Header("Debug")]
    public bool logHover = true;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when the button's hover is exited.
    /// </summary>
    public bool logExit = true;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when the button is clicked.
    /// </summary>
    public bool logClick = true;

    public void OnPointerEnter(PointerEventData eventData) {
      if (logHover) {
        Debug.Log("Hovered over button!");
      }
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (logExit) {
        Debug.Log("Exited hovering button!");
      }
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (logClick) {
        Debug.Log("Clicked button!");
      }
    }
  }
}