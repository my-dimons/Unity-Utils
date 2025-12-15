using UnityEngine;

namespace UnityUtils.ScriptUtils
{
    public static class CoroutineStarter
    {
        public class CoroutineStarterObject : MonoBehaviour { }
        private static CoroutineStarterObject starter;
        public static CoroutineStarterObject Starter { 
            get 
            { 
                if (starter == null)
                {
                    GameObject obj = new GameObject("Coroutine Starter (UnityUtils)");
                    starter = obj.AddComponent<CoroutineStarterObject>();
                    Object.DontDestroyOnLoad(obj);
                }

                return starter;
            } 
        }
    }
}