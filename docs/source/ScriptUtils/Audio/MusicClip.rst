MusicClip
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
     
An abstract class that is used in :doc:`MusicManager`. Has a :doc:`BackgroundMusic` which acts as an example, and also will always be played.

Example Usage
-------------
.. code:: csharp
  
  [CreateAssetMenu(fileName = "BackgroundMusic", menuName = "UnityUtils/Audio/Background Music", order = 0)]
  public class BackgroundMusic : MusicClip 
  {
    public override bool CanBePlayed() 
    {
      // Add logic to decide if this clip can be played
      return true;
    }
  }   
  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::MusicClip
   :members: