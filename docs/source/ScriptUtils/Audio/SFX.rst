SFX
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
     
The **SFX** class is used to easily play sound effects in turn with :doc:`SFXManager`.


Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio;
   
   public class ExampleScript : MonoBehaviour
   {
   	public SFX sfxClip = SFX.Create2dSFX(); // Creates a new SFX clip with some default parameters for a 2d SFX
   	public SFX musicClips = SFX.Create3dSFX(); // Creates a new SFX clip with some default parameters for Music clips

   	void Start()
   	{
   	   // Play basic SFX
   	   SfxManager.PlaySFX(sfxClip);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::SFX
   :members: