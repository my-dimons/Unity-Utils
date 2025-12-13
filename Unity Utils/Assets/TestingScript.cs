using UnityEngine;
using UnityUtils.ScriptUtils;

public class TestingScript : MonoBehaviour
{
    public float testingValue;
    public Vector3 testingVector3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectAnimations.AnimateTransformRotation(transform, new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z), new Vector3(4, 50, 90), 2);
        ObjectAnimations.AnimateTransformScale(transform, transform.localScale, new Vector3(3, 4, 5), 2);
        ObjectAnimations.AnimateTransformPosition(transform, transform.position, new Vector3(0, 5, 0), 2);

        ObjectAnimations.AnimateSpriteRendererOpacity(GetComponent<SpriteRenderer>(), 0.5f, 1, 2);
    }
}