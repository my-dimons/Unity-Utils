using System;
using System.Collections;
using UnityEngine;
using UnityUtils.ScriptUtils;
using UnityUtils.ScriptUtils.Audio;

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
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(1);

    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
