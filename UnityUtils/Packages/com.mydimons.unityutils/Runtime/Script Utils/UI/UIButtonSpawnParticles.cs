using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Particles;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonSpawnParticles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    /// <summary>
    /// Will spawn all prefabs in this array on hover
    /// </summary>
    [Header("Particles")]
    public GameObject[] hoverParticlePrefabs;

    /// <summary>
    /// Will spawn all prefabs in this array on hover exit
    /// </summary>
    public GameObject[] exitParticlePrefabs;

    /// <summary>
    /// Will spawn all prefabs in this array on click
    /// </summary>
    public GameObject[] clickParticlePrefabs;

    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when a particle is spawned
    /// </summary>
    [Header("Debug")]
    public bool logSpawn;

    public void OnPointerEnter(PointerEventData eventData) {
      SpawnParticles(hoverParticlePrefabs);
    }

    public void OnPointerExit(PointerEventData eventData) {
      SpawnParticles(exitParticlePrefabs);
    }

    public void OnPointerClick(PointerEventData eventData) {
      SpawnParticles(clickParticlePrefabs);
    }

    private void SpawnParticles(GameObject[] particles) {
      foreach (GameObject particle in particles) {
        ParticleSpawner.SpawnBurstParticle(particle, transform.position);
      }

      if (logSpawn) {
        Debug.Log("Spawned particle system: " + particles);
      }
    }
  }
}