using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils;

using UnityEngine.SceneManagement;

public class TestingScript2 : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
