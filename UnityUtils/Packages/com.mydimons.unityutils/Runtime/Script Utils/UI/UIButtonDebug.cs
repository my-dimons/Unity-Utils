using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonDebug : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public UIButtonDebugSettings logSettings;
    public bool hoveringOverButton;

    public void OnPointerEnter(PointerEventData eventData) {
      if (logSettings.logIn) {
        Debug.Log("Hovered over button!");
      }

      LogAny();

      hoveringOverButton = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (logSettings.logOut) {
        Debug.Log("Exited hovering button!");
      }

      LogAny();

      hoveringOverButton = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (logSettings.logClick) {
        Debug.Log("Clicked button!");
      }

      LogAny();
    }

    private void LogAny() {
      if (logSettings.logAny) {
        Debug.Log("Button 'any' log!");
      }
    }
  }
}
