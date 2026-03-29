using UnityEngine;

namespace UnityUtils.ScriptUtils.UI {
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

  [System.Serializable]
  public class UIButtonDebugSettings {
    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when the button's "In" function is started. Usually on hover enter
    /// </summary>
    public bool logIn = false;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when the button's "Out" function is started. Usually on hover exit
    /// </summary>
    public bool logOut = false;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when the button's "Click" function is started. Usually on click
    /// </summary>
    public bool logClick = false;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> on any of the button's "In", "Out", or "Click" functions.
    /// </summary>
    public bool logAny = false;

    /// <summary>
    /// A default function that can called and will output a <see cref="Debug.Log(object)"/> if <see cref="logIn"/> is true, or if <see cref="logAny"/> is true.
    /// </summary>
    public void LogIn() {
      if (logIn)
        Debug.Log("Button In function started.");

      LogAny();
    }

    /// <summary>
    /// A default function that can called and will output a <see cref="Debug.Log(object)"/> if <see cref="logOut"/> is true, or if <see cref="logAny"/> is true.
    /// </summary>
    public void LogOut() {
      if (logOut)
        Debug.Log("Button Out function started.");

      LogAny();
    }

    /// <summary>
    /// A default function that can called and will output a <see cref="Debug.Log(object)"/> if <see cref="logClick"/> is true, or if <see cref="logAny"/> is true.
    /// </summary>
    public void LogClick() {
      if (logClick)
        Debug.Log("Button Click function started.");

      LogAny();
    }

    /// <summary>
    /// A default function that can called and will output a <see cref="Debug.Log(object)"/> if <see cref="logAny"/> is true. Is called from all the other log functions.
    /// </summary>
    public void LogAny() {
      if (logAny)
        Debug.Log("Button 'any' debug.");
    }
  }
}