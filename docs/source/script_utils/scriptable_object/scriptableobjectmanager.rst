ScriptableObjectManager
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.ScriptableObjects`
  
.. note::

   Make sure your Scriptable Objects are somewhere withing the 'Resources' folder in 'Assets' (If you don't have a 'Resources folder, create one).
   
The **ScriptableObjectManager** is used to easily grab scriptable objects from a location in the Unity file system.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.ScriptableObjects;
   
   public class ExampleScript : MonoBehaviour
   {	
   	void Start()
   	{
   	   // Grabs all scriptable objects in the 'Assets/Resources/Weapons folder' and sub folders
   	   // (Example 'Weapon' class)
   	   Weapon[] weapons = ScriptableObjectManager.GetScriptableObjects("Weapons");
   	   
   	   // Grabs all scriptable objects in the 'Assets/Resources/Weapons/Guns' folder and sub folders
   	   // (Example 'Gun' class)
   	   Gun[] guns = ScriptableObjectManager.GetScriptableObjects("Weapons/Guns");
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::ScriptableObjects::ScriptableObjectManager
   :members: