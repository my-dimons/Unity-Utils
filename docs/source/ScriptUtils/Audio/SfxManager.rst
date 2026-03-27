SFXManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
     
The **SFXManager** is used to easily play sound effects using the :doc:`SFX` with some set parameters. Allows you to easily add in a pitch variance so clips don't feel as repetitive, and randomize SFX between a few clips.

* Default pitch variance is `0.1`.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio;
   
   public class ExampleScript : MonoBehaviour
   {
   	public SFX clip = SFX.CreateSFX();

   	void Start()
   	{
   	   // Play basic SFX
   	   SFXManager.PlaySFX(clip);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::SfxManager
   :members: