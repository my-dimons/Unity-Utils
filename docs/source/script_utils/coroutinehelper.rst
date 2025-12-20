CoroutineHelper
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils`
     
The **CoroutineHelper** script is used to start coroutines in a static class.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils;
   using System;
   using System.Collections;
   
   public static class ExampleScript
   {
   	void ExampleFunction()
   	{
   	   // Starts a normal coroutine
   	   CoroutineHelper.Starter.StartCoroutine(ExampleCoroutine());
   	   
   	   // Starts a coroutine that won't stop on scene change (Be careful when using this, can cause lots of errors)
   	   CoroutineHelper.PersistantStarter.StartCoroutine(ExampleCoroutine());
   	}
   	
   	public IEnumerator ExampleCoroutine()
   	{
   	   yield return new WaitForSeconds(3f);
   	   Debug.Log("Coroutine Ended");
   	}
   }  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::CoroutineHelper
   :members:
   :exclude-members: Starter