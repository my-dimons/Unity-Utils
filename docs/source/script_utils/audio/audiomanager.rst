AudioManager
============

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
   
.. note::

   This script requires :doc:`AudioManager` to function properly.
   
The **AudioManager** is used for managing audio types for things like sfx, and music. It provides functions for calculating volume type so you can 

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio.AudioManager;
   
   public class ExampleScript : MonoBehaviour
   {
     void Start()
     {
        // Sets the sfx volume type to 0.4 volume.
        AudioManager.SetVolume(VolumeType.sfx, 0.4f);
        
        // Modifies the music volume type by -0.1f. If music volume starts at 1f, the new volume would be 0.9f.
        AudioManager.ModifyVolume(VolumeType.music, -0.1f);
        
        // Grabs a volume types set volume.
        float exampleVolumeValue = AudioManager.GetVolume(VolumeType.global);
        
        // Multiply a volume by a volume types volume (For proper adjustments)
        float exampleVolueValue2 = AudioManager.CalculateVolumeBasedOnType(1, 
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::BackgroundMusicManager
   :members: