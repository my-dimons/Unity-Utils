CameraShake
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Cameras`
     
The **CameraShake** is used to easily add screenshake the camera

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Cameras;
   
   public class ExampleScript : MonoBehaviour
   {  	
   	void Start()
   	{
   	   CameraShake.Screenshake(intensity: 5, duration: 5);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Camera::CameraShake
   :members: