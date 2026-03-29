MusicManager
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
   
The **MusicManager** is used for quickly adding music to your games. It allows you to cycle through random songs with some adjustable properties for fading songs and have random delay between songs.

Also has some Actions that can be called! Is very useful for having a system that displays something when a new music track plays.

.. warning::
   Turn on "Run in Background" in the `Unity settings <https://discussions.unity.com/t/how-do-you-keep-your-game-  running-even-when-you-switch-out-of-it/928>`_, 
   if you don't the script may bug out when tabbing in/out of the game.
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio;
   
   public class ExampleScript : MonoBehaviour
   {
     void Start()
     {
   	   // Start playing music.
   	   MusicManager.Instance.StartContinuousMusic();
   	      	   
   	   // Stop playing music.
   	   MusicManager.Instance.StopContinuousMusic();
   	}
   	
   	void OnEnable()
   	{
   	   MusicManager.Instance.OnPlayMusicTrack += SomeFunction;
   	   MusicManager.Instance.OnStopMusicTrack += SomeOtherFunction;
   	}
   	
   	void OnEnable()
   	{
   	   MusicManager.Instance.OnPlayMusicTrack -= SomeFunction;
   	   MusicManager.Instance.OnStopMusicTrack -= SomeOtherFunction;
   	}
   }
   

.. tip::
   
   Use .wav format audio for better audio quality and looping!
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::MusicManager
   :members:
