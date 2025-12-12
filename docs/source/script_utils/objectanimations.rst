ObjectAnimations
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils`
     
The **ObjectAnimations** script is used to help with animating object values, things like position, script values, etc.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils;
   
   public class ExampleScript : MonoBehaviour
   {
   	public AnimationCurve animationCurve;
   	public bool useRealtime;
   	
	public float testingValue;
	public Vector3 testingVector3;
	
   	void Start()
   	{
        // Animates an objects rotation value from its current rotation to (4, 50, 90) in 2 seconds.
        ObjectAnimations.AnimateTransformRotation(transform, new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z), new Vector3(4, 50, 90), 2, useRealtime, animationCurve);
        
        // Animates an objects scale  value from its current rotation to (3, 4, 5) in 2 seconds.
        ObjectAnimations.AnimateTransformScale(transform, transform.localScale, new Vector3(3, 4, 5), 2, useRealtime, animationCurve);
        
        // Animates an objects position value from its current rotation to (0, 5, 0) in 2 seconds.
        ObjectAnimations.AnimateTransformPosition(transform, transform.position, new Vector3(0, 5, 0), 2, useRealtime, animationCurve);
        
        
        // Animates the testingValue variable's value from 0 to 1 in 2 seconds.
        ObjectAnimations.AnimateValue(0, 1, 2, value => testingValue = value, useRealtime, animationCurve);
        
        // Animates the testingVector3 variable's value from (0, 0, 0) to (0, 1, 4) in 2 seconds.
        ObjectAnimations.AnimateVector3Value(Vector3.zero, new Vector3(0, 1, 4), 2, value => testingVector3= value, useRealtime, animationCurve);
     }
  }
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::ObjectAnimations
   :members: