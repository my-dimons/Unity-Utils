using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils
{
    public static class ObjectAnimations
    {
        #region This region is assisted by ChatGPT
        // Had to somehow have it start coroutines without someone manually assigning a script
        private class ObjectAnimationsCoroutineStarter : MonoBehaviour { }
        private static ObjectAnimationsCoroutineStarter coroutineStarter;
        private static ObjectAnimationsCoroutineStarter CoroutineStarter
        {
            get
            {
                if (coroutineStarter == null)
                {
                    GameObject starter = new GameObject("Unity Utils - Coroutine Starter");
                    coroutineStarter = starter.AddComponent<ObjectAnimationsCoroutineStarter>();
                    UnityEngine.Object.DontDestroyOnLoad(starter);
                }

                return coroutineStarter;
            }
        }
        #endregion

        /// <summary>
        /// Animates an objects <see cref="Transform"/> components scale from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateTransformScale(Transform transform, Vector3 startScale, Vector3 endScale, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            AnimateVector3Value(startScale, endScale, duration,
                value => transform.localScale = value,
                useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="Transform"/> components position from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateTransformPosition(Transform transform, Vector3 startPos, Vector3 endPos, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            AnimateVector3Value(startPos, endPos, duration,
                value => transform.position = value,
                useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="Transform"/> components rotation from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateTransformRotation(Transform transform, Vector3 startRotation, Vector3 endRotation, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            AnimateVector3Value(startRotation, endRotation, duration,
                value => transform.localRotation = Quaternion.Euler(value),
                useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="SpriteRenderer"/> component alpha from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateSpriteRendererOpacity(SpriteRenderer spriteRenderer, float startOpacity, float endOpacity, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            Color color = spriteRenderer.color;

            AnimateValue(startOpacity, endOpacity, duration,
                value => spriteRenderer.color = new Color(color.r, color.g, color.b, value),
                useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="Image"/> component alpha from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateImageOpacity(Image image, float startOpacity, float endOpacity, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            Color color = image.color;

            AnimateValue(startOpacity, endOpacity, duration,
                value => image.color = new Color(color.r, color.g, color.b, value),
                useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates a <see cref="Vector3"/> value from a starting value to an ending value over a specified duration.
        /// </summary>
        /// <param name="onValueChanged">A callback that is invoked with the current interpolated Vector3 value as the animation progresses.</param>
        /// <param name="useRealtime">true to use unscaled real time for the animation (ignoring time scale).</param>
        /// <param name="animationCurve">Default is a linear curve.</param>
        public static void AnimateVector3Value(Vector3 startValue, Vector3 endValue, float duration, Action<Vector3> onValueChanged, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
            CoroutineStarter.StartCoroutine(AnimateVector3ValueCoroutine(startValue, endValue, duration, animationCurve, onValueChanged, useRealtime));
        }

        /// <summary>
        /// Animates a float value from a starting value to an ending value over a specified duration
        /// </summary>
        /// <param name="onValueChanged">A callback that is invoked with the current interpolated Vector3 value as the animation progresses.</param>
        /// <param name="useRealtime">true to use unscaled real time for the animation (ignoring time scale)</param>
        /// <param name="animationCurve">Default is a linear curve</param>
        public static void AnimateValue(float startValue, float endValue, float duration, Action<float> onValueChanged, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
            CoroutineStarter.StartCoroutine(AnimateValueCoroutine(startValue, endValue, duration, animationCurve, onValueChanged, useRealtime));
        }

        private static IEnumerator AnimateValueCoroutine(float start, float end, float duration, AnimationCurve curve, Action<float> onValueChanged, bool useRealtime = false)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += useRealtime ? Time.unscaledDeltaTime : Time.deltaTime;

                float time = Mathf.Clamp01(elapsed / duration);
                float value = Mathf.Lerp(start, end, curve.Evaluate(time));

                onValueChanged?.Invoke(value);

                yield return null;
            }

            // Ensure final value
            onValueChanged?.Invoke(end);
        }


        private static IEnumerator AnimateVector3ValueCoroutine(Vector3 start, Vector3 end, float duration, AnimationCurve curve, Action<Vector3> onValueChanged, bool useRealtime = false)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += useRealtime ? Time.unscaledDeltaTime : Time.deltaTime;

                float time = Mathf.Clamp01(elapsed / duration);
                float x = Mathf.Lerp(start.x, end.x, curve.Evaluate(time));
                float y = Mathf.Lerp(start.y, end.y, curve.Evaluate(time));
                float z = Mathf.Lerp(start.z, end.z, curve.Evaluate(time));

                onValueChanged?.Invoke(new Vector3(x, y, z));

                yield return null;
            }

            // Ensure final value
            onValueChanged?.Invoke(end);
        }
    }
}