using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Audio;
using UnityUtils.ScriptUtils.Particles;
using static UnityUtils.ScriptUtils.Audio.AudioManager;

namespace UnityUtils.ScriptUtils.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButtonToggleObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Objects")]

        /// Will spawn all prefabs in this array on hover
        public GameObject[] hoverToggleObjects;

        /// Will spawn all prefabs in this array on hover exit
        public GameObject[] exitToggleObjects;

        /// Will spawn all prefabs in this array on click
        public GameObject[] clickToggleObjects;

        [Header("Debug")]
        public bool logToggle;

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToggleObjects(hoverToggleObjects);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToggleObjects(exitToggleObjects);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ToggleObjects(clickToggleObjects);
        }

        private void ToggleObjects(GameObject[] objects)
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(!obj.activeSelf);
            }

            if (logToggle)
            {
                Debug.Log("Spawned particle system: " + objects);
            }
        }
    }
}