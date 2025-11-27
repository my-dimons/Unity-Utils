BackgroundMusicManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`

.. note::

   This script requires :doc:`AudioManager` to function properly.
   
The **BackgroundMusicManager** is used for quickly adding music to your games. It allows you to cycle through random songs with some adjustable properties for fading songs and have random delay between songs.

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
   	   // Start playing music.
   	   manager.StartContinuousMusic();
   	      	   
   	   // Stop playing music.
   	   manager.StopMusic();
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::BackgroundMusicManager
   :members:
