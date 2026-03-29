BackgroundMusic
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
     
An example of how to use the :doc:`MusicClip`. To use it, create a scriptable object (Found in the UnityUtils/Audio/Background Music path when right clicking in the "Project" tab in Unity) and add adjust its values. Then drag this scriptable object into a :doc:`MusicManager`.

.. tip::
	
   This script is still very useful, being a music clip that will can *always* play.
   
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

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::BackgroundMusic
   :members: