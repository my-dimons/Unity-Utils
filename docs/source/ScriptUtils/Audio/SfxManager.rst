SfxManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
  
.. note::

   This script requires :doc:`AudioManager` to function properly.
   
The **SfxManager** is used to easily play sound effects with preset volumes from the :doc:`AudioManager` script, and add random pitch variance.

* Default pitch variance is `0.1`.

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
   	   // Play basic SFX
   	   SfxManager.PlaySfxAudioClip(clip, 1, 0.6);
   	   
   	   // Play sfx clip for set amount of time
   	   SfxManager.PlayTimedSFXAudioClip(clip, 1);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::SfxManager
   :members: