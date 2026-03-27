SimpleBackgroundMusic
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
   
The **SimpleBackgroundMusic** is used for quickly adding music to your games. It allows you to cycle through random songs with some adjustable properties for fading songs and have random delay between songs.

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
   	   SimpleBackgroundMusic.Instance.StartContinuousMusic();
   	      	   
   	   // Stop playing music.
   	   SimpleBackgroundMusic.Instance.StopContinuousMusic();
   	}
   }
   

.. tip::
   
   Use .wav format audio for better audio quality and looping!
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::SimpleBackgroundMusic
   :members:
