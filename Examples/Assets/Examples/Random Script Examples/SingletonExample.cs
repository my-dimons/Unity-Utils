using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    public static SingletonExample Instance { get; private set; }

    private void Awake()
    {
        // PUT SINGLETONS HERE (SINGLETONS MUST BE IN AWAKE)
    }

    /// <summary>
    /// Holds examples of multiple singleton patterns, to be used in the Awake() function
    /// Pick whichever one you like best! (I prefer the Basic Singleton Pattern (Compressed)!)
    /// </summary>
    private void SingletonPatterns()
    {
        #region Singleton Pattern Example 1 - Basic if/else Singleton

        // Basic Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }

        // Basic Singleton Pattern (Compressed)
        if (Instance == null) Instance = this; else Destroy(gameObject);

        #endregion
    }
}