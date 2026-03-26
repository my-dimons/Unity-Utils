using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonQuitGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    /// <summary>
    /// Will quit the application on hover
    /// </summary>
    [Header("Objects")]
    public bool hoverQuit = false;

    /// <summary>
    /// Will quit the application on hover exit
    /// </summary>
    public bool exitQuit = false;

    /// <summary>
    /// Will quit the application on click
    /// </summary>
    public bool clickQuit = true;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when quitting the application.
    /// </summary>
    [Header("Debug")]
    public bool logQuit;

    public void OnPointerEnter(PointerEventData eventData) {
      if (hoverQuit)
        QuitGame();
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (exitQuit)
        QuitGame();
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (clickQuit)
        QuitGame();
    }

    private void QuitGame() {
      if (logQuit)
        Debug.Log("Quitting Application");

      Application.Quit();
    }
  }
}