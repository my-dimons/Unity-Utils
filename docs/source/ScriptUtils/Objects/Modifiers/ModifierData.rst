ModifierData
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects.Modifiers`
     
The **ModifierData** script is used in turn with :doc:`ObjectModifiers` to apply modifiers to variables

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
	private ObjectModifierData<float> data;
	
   	void Start()
   	{
   	   // Creates a new ModifierData object
   	   data = new ModifierData<float>(ModifierType.Divide, 3))
   	}
   }
  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::Modifiers::ObjectModifierData
   :members: