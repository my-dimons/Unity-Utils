ParticleManager
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Particles`
     
The **ParticleManager** is used to easily play and edit particle systems.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Particles;
   
   public class ExampleScript : MonoBehaviour
   {
   	public GameObject particlePrefab;
   	public GameObject particles;
   	
   	void Start()
   	{
   	   // Spawn burst particles.
   	   ParticleManager.SpawnBurstParticles(particlePrefab, Vector3.zero);
   	   
   	   // Set particle system colour.
   	   ParticleManager.SetParticleSystemColor(particles, Color.blue);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Particles::ParticleManager
   :members: