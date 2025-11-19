using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    /// <summary>
    /// Spawns a burst particle prefab at the given position
    /// Has adjustable color or gradient, but doesn't need to be inputted
    /// </summary>
    /// <param name="particlePrefab">Particle system to spawn (Gameobject with particle system applied)</param>
    /// <param name="position">Position to spawn the particlePrefab at</param>
    /// <param name="color">Color to set the particlePrefab to</param>
    /// <param name="gradient">Gradient to set the particlePrefab to</param>
    public void SpawnBurstParticle(GameObject particlePrefab, Vector3 position, Color color = default, Gradient gradient = default)
    {
        GameObject particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
        ParticleSystem ps = GetParticleSystem(particleInstance);

        if (color != default) SetParticleSystemColor(particleInstance, color);
        else if (gradient != default) SetParticleSystemGradientColor(particleInstance, gradient);

        ps.Play();

        float particleLife = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(particleInstance, particleLife);
    }

    private void SetParticleSystemColor(GameObject particleSystem, Color color)
    {
        var main = GetParticleSystem(particleSystem).main;
        main.startColor = color;
    }

    private void SetParticleSystemGradientColor(GameObject particleSystem, Gradient gradient)
    {
        var main = GetParticleSystem(particleSystem).main;
        main.startColor = gradient;
    }

    /// <summary>
    /// Tries to get the particle system on the given object, prints a warning message if it doesn't exist and returns null
    /// </summary>
    private ParticleSystem GetParticleSystem(GameObject obj)
    {
        if (!obj.TryGetComponent<ParticleSystem>(out var ps))
        {
            Debug.LogWarning("The \"" + name + "\" Prefab has no ParticleSystem component!");
            return null;
        }
        return ps;
    }
}
