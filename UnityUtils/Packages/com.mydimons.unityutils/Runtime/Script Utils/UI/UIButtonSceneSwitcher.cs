using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UnityUtils.ScriptUtils.UI {
  public class UIButtonSceneSwitcher : MonoBehaviour, IPointerClickHandler {

    /// <summary>
    /// If this value is not empty, it will load the scene on click using the sceneName 
    /// </summary>
    [Header("Scene Loading (Only use 1 of the below variables, leave 1 default)")]
    public string sceneName = "";

    /// <summary>
    /// If this value is not -1 (Default value), it will load the scene using the buildIndex
    /// </summary>
    public int buildIndex = -1;


    /// <summary>
    /// The <see cref="LoadSceneMode"/> to use when loading the scene
    /// </summary>
    [Header("Scene Mode")]
    public LoadSceneMode sceneMode;


    /// <summary>
    /// Will output a <see cref="Debug.Log(object)"/> when switching scenes
    /// </summary>
    [Header("Debug")]
    public bool logSwitch;

    public void OnPointerClick(PointerEventData eventData) {
      LoadScene();
    }

    private void LoadScene() {
      bool useSceneName = sceneName != "";
      bool useBuildIndex = buildIndex != -1;

      if (useSceneName && !useBuildIndex)
        SceneManager.LoadScene(sceneName, sceneMode);
      else if (useBuildIndex && !useSceneName)
        SceneManager.LoadScene(buildIndex, sceneMode);
      else
        Debug.Log("Cannot load scene, sceneName and buildIndex are both being used. Change one value to be null to load the scene");

      if (logSwitch)
        Debug.Log("Switching Scenes");
    }
  }
}