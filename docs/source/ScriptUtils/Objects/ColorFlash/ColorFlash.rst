ColorFlash
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects.ColorFlash`
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
   	public ColorFlash flash = ColorFlash.CreateDefaultFlash();
   	public ColorFlashManager flashManager;
   	
   	void Start()
   	{
   	   // Flash the object with a ColorFlashManager applied using the "flash"
   	   flashManager.Flash(flash);
   	}
  }
     
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ColorFlash::ColorFlash
   :members: