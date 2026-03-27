ColorFlashManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects.ColorFlash`
     
Apply a **ColorFlashManager** script to an object and call its functions to flash it a certain color for a certain time. 
It works by changing the material of the specified object and changing the new materials color, then switching back to the default material after its done.

.. warning::

   Do not modify a objects material while flashing, as it will interfere with the flashing. You can check if an object is flashing by calling IsFlashing().
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
   	public ColorFlashManager object;
   	public Material otherMat;
   	
   	void Start()
   	{
   	   // Flashes the applied object white for 2 seconds.
   	   object.Flash(duration: 2);
   	   
   	   // Flashes the applied object blue for 2 seconds.
   	   object.Flash(Color.blue, 2);
   	   
   	   // Set the original material that is switched to after flashing
   	   object.SetOriginalMaterial(otherMat);
   	   
   	   // Check if the object is currently flashing
   	   bool isFlashing = object.IsFlashing();
   	}
  }
  
  
.. warning::

   ColorFlashManager only currently works with SpriteRenderers, but other compatibility is coming soon.
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ColorFlash::ColorFlashManager
   :members: