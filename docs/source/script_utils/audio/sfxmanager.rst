SFXManager
==========

**NAMESPACE:**
.. code:: csharp

   UnityUtils.ScriptUtils.Audio
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio;
   public class ExampleScript : MonoBehaviour
   {
   	public AudioClip clip;

   	void Start()
   	{
   	   // play basic SFX
   	   SfxManager.PlaySfxAudioClip(clip, 1, 0.6);
   	   
   	   // play sfx clip for set amount of time
   	   SfxManager.PlayTimedSFXAudioClip(clip, 1);
   	}