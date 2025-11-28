BackgroundMusicManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`

.. note::

   This script requires :doc:`AudioManager` to function properly.
   
The **BackgroundMusicManager** is used for quickly adding music to your games. It allows you to cycle through random songs with some adjustable properties for fading songs and have random delay between songs.

.. tip::
   
   * Use .wav format audio for better audio quality.
   * Turn on "Run in Background" in the `Unity settings <https://discussions.unity.com/t/how-do-you-keep-your-game-running-even-when-you-switch-out-of-it/928>`_, if you don't the script may bug out when tabbing out and into the game.
   
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
