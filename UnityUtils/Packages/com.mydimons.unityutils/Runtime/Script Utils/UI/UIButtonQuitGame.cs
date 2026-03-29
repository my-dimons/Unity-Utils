using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonQuitGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    /// <summary>
    /// Specifies the user interaction methods available for a <see cref="UIButtonQuitGame"/>
    /// </summary>
    public enum QuitGameButtonMethod {
      /// <summary>
      /// Quit on hover enter
      /// </summary>
      HoverEnter,
      /// <summary>
      /// Quit on hover exit
      /// </summary>
      HoverExit,
      /// <summary>
      /// Quit on hover click
      /// </summary>
      Click
    }

    /// <summary>
    /// Will quit the game based on the <see cref="QuitGameButtonMethod"/>.
    /// </summary>
    public QuitGameButtonMethod quitMethod = QuitGameButtonMethod.Click;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when quitting the application.
    /// </summary>
    [Header("Debug.Logs()")]
    public bool logQuit;

    public void OnPointerEnter(PointerEventData eventData) {
      if (quitMethod == QuitGameButtonMethod.HoverEnter)
        QuitGame();
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (quitMethod == QuitGameButtonMethod.HoverExit)
        QuitGame();
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (quitMethod == QuitGameButtonMethod.Click)
        QuitGame();
    }

    private void QuitGame() {
      if (logQuit)
        Debug.Log("Quitting Application");

      Application.Quit();
    }
  }
}