using System;
using UnityEngine;

/// <summary>
/// Holds variables/functions for other audio scripts (Such as global volume)
/// </summary>
public static class AudioManager
{
    // To add more volume types, add more properties to this array and then update GetVolumeBasedOnType()
    public enum AudioType
    {
        sfx,
        music,
        global
    }

    [Range(0f, 1f)]
    [Tooltip("All volume is multiplied by this value")]
    public static float globalVolume;

    [Space(8)]

    [Range(0f, 1f)]
    [Tooltip("Music volume is multiplied by this value")]
    public static float musicVolume;

    [Range(0f, 1f)]
    [Tooltip("SFX volume is multiplied by this value")]
    public static float sfxVolume;

    /// <summary>
    /// Does multiplication to certain volume types (To get right audio levels)
    /// </summary>
    /// <param name="baseVolume">Volume to convert</param>
    /// <param name="type">Type of audio, use AudioType.global to just multiply by global volume</param>
    /// <returns></returns>
    public static float GetVolumeBasedOnType(float baseVolume, AudioType type)
    {
        float baseVolumeMutliplied = baseVolume * globalVolume;

        switch (type)
        {
            case AudioType.sfx: return baseVolumeMutliplied * sfxVolume;
            case AudioType.music: return baseVolumeMutliplied * musicVolume;
            case AudioType.global: return baseVolumeMutliplied;

            default: return baseVolumeMutliplied;
        }
    }
}

/// <summary>
/// Holds variables/functions for other audio scripts (Such as global volume), but is a singleton so it can be used in the unity editor
/// </summary>
/*public class AudioManagerObject : MonoBehaviour
{
    public class Testing
    {

    }
    public static AudioManagerObject Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}*/
