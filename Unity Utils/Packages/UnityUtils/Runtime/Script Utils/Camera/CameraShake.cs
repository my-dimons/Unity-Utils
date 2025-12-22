using UnityEngine;
using System.Collections;

namespace UnityUtils.ScriptUtils.Cameras
{
    public static class CameraShake
    {
        /// Default curve used when 
        public static AnimationCurve defaultScreenshakeCurve = new AnimationCurve(
        new Keyframe(0, 0),
        new Keyframe(0.2f, 0.1f, -0.05f, -0.05f),
        new Keyframe(1, 0)
        );

        public static void Screenshake(Transform camera = default, float intensity = 1, float duration = 0.5f, AnimationCurve curve = default, bool useRealtime = true)
        {
            if (curve == default) curve = defaultScreenshakeCurve;
            if (camera == default) camera = Camera.main.transform;

            CoroutineHelper.Starter.StartCoroutine(ScreenshakeCoroutine(camera, intensity, duration, curve, useRealtime));
        }
        public static IEnumerator ScreenshakeCoroutine(Transform camera, float intensity, float duration, AnimationCurve curve, bool useRealtime)
        {
            Vector3 startPosition = camera.localPosition;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += useRealtime ? Time.unscaledDeltaTime : Time.deltaTime;
                float strength = curve.Evaluate(elapsedTime / duration) * intensity;
                camera.localPosition = startPosition + Random.insideUnitSphere * strength;
                yield return null;
            }

            camera.localPosition = startPosition;
        }
    }
}
