using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonToggleObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    /// <summary>
    /// Will toggle all objects's active state in this array on hover
    /// </summary>
    [Header("Objects")]
    public GameObject[] hoverToggleObjects;

    /// <summary>
    /// Will toggle all objects's active state in this array on hover exit
    /// </summary>
    public GameObject[] exitToggleObjects;

    /// <summary>
    /// Will toggle all objects's active state in this array on click
    /// </summary>
    public GameObject[] clickToggleObjects;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when toggling objects
    /// </summary>
    [Header("Debug")]
    public bool logToggle;

    public void OnPointerEnter(PointerEventData eventData) {
      ToggleObjects(hoverToggleObjects);
    }

    public void OnPointerExit(PointerEventData eventData) {
      ToggleObjects(exitToggleObjects);
    }

    public void OnPointerClick(PointerEventData eventData) {
      ToggleObjects(clickToggleObjects);
    }

    private void ToggleObjects(GameObject[] objects) {
      foreach (GameObject obj in objects) {
        obj.SetActive(!obj.activeSelf);
      }

      if (logToggle) {
        Debug.Log("Toggled objects: " + objects);
      }
    }
  }
}