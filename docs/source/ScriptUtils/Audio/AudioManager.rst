AudioManager
============

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Audio`
   
.. note::

   This script requires :doc:`AudioManager` to function properly.
   
The **AudioManager** is used for managing audio types for things like sfx, and music. It provides functions for calculating volume based on a volume type so you can easily play sounds at a consistent volume!

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Audio;
   
   public class ExampleScript : MonoBehaviour
   {
     void Start()
     {
        // Sets the sfx volume type to 0.4 volume.
        AudioManager.SetVolume(AudioManager.VolumeType.sfx, 0.4f);
        
        // Modifies the music volume type by -0.1f. If music volume starts at 1f, the new volume would be 0.9f.
        AudioManager.ModifyVolume(AudioManager.VolumeType.music, -0.1f);
        
        // Grabs a volume types set volume.
        float exampleVolumeValue = AudioManager.GetVolume(AudioManager.VolumeType.global);
        
        // Multiply a volume by a volume types volume (For proper adjustments)	
        float exampleVolueValue2 = AudioManager.CalculateVolumeBasedOnType(0.3f, AudioManager.VolumeType.sfx);
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Audio::AudioManager
   :members: