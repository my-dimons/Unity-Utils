using System.Collections;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public AudioClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SfxManager.Instance.PlayTimedSFXAudioClip(clip, 1);
        StartCoroutine(TestCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Should stop playing audio now");
    }
}
