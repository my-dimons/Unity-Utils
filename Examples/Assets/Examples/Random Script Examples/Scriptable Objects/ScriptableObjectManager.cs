using UnityEngine;

public static class ScriptableObjectManager
{
    /// <summary>
    /// Gets an array of scriptable objects found in [Resources/'path'], make sure you have a 'Resources' folder created
    /// </summary>
    /// <param name="path">file path to where scriptable objects are held (found in [Resources/'path']</param>
    /// <returns>Array of specified scriptable object type</returns>
    public static T[] LoadScriptableObjects<T>(string path) where T : ScriptableObject
    {
        T[] loadedObjects = Resources.LoadAll<T>(path);

        if (loadedObjects.Length <= 0)
        {
            Debug.LogWarning("No commands found in Resources/" + path + " folder.");
            return default;
        }
        
        foreach (T obj in loadedObjects)
        {
            Debug.Log("Loaded ScriptableObject: " + obj.name);
        }

        return loadedObjects;
    }
}
