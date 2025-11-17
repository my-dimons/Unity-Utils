using UnityEngine;

public static class ParticleManager : MonoBehaviour
{
    public static void SpawnBurstParticle(GameObject particlePrefab, Vector3 position)
    {
        GameObject particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);

        // Play it (in case it's not already set to play on awake)
        ps.Play();

        // Schedule destruction when it's done
        Destroy(particleInstance, ps.main.duration + ps.main.startLifetime.constantMax);
    }

    //private bool ObjectHasParticleSystem(GameObject obj)
    //{
    //    if (!particleInstance.TryGetComponent<ParticleSystem>(out var ps))
    //    {
    //        Debug.LogWarning("The \"" + obj.name + "\" Prefab has no ParticleSystem component!");
    //        return;
    //    }
    //}

    private void SetParticleSystemColor(GameObject particleSystem, Color color)
    {
        if (!particleInstance.TryGetComponent<ParticleSystem>(out var ps))
        {
            Debug.LogWarning("The \"" particleSystem.name + "\" Prefab has no ParticleSystem component!");
            Destroy(particleInstance);
            return;
        }

        var main = ps.main;
        main.startColor = color;
    }
}
