using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils;

public class TestingScript1 : MonoBehaviour
{
    public float testingValue;
    public Vector3 testingVector3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectAnimations.AnimateImageOpacity(GetComponent<Image>(), 0.5f, 1, 2);
    }
}
