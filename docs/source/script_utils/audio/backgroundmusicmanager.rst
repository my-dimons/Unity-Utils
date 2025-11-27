BackgroundMusicManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio;
   
   public class ExampleScript : MonoBehaviour
   {
     public BackgroundMusicManager manager;
     void Start()
     {
   	   // start playing music
   	   manager.StartContinuousMusic();
   	      	   
   	   // stop playing music
   	   manager.StopMusic();
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::BackgroundMusicManager
   :members:
