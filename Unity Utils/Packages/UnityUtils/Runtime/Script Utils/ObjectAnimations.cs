using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils;

namespace UnityUtils.ScriptUtils
{
    public static class ObjectAnimations
    {
        #region Transforms
        /// <summary>
        /// Animates an objects <see cref="Transform"/> components scale from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateTransformScale(Transform transform, Vector3 startScale, Vector3 endScale, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            AnimateValue<Vector3>(startScale, endScale, duration, (a, b, t) => Vector3.Lerp(a, b, t), value => transform.localScale = value, useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="Transform"/> components position from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateTransformPosition(Transform transform, Vector3 startPos, Vector3 endPos, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            AnimateValue<Vector3>(startPos, endPos, duration, (a, b, t) => Vector3.Lerp(a, b, t), value => transform.position = value, useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="Transform"/> components rotation from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateTransformRotation(Transform transform, Vector3 startRotation, Vector3 endRotation, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            AnimateValue<Vector3>(startRotation, endRotation, duration, (a, b, t) => Vector3.Lerp(a, b, t), value => transform.localRotation = Quaternion.Euler(value), useRealtime, animationCurve);
        }
        #endregion

        #region Opacity
        /// <summary>
        /// Animates an objects <see cref="SpriteRenderer"/> component alpha from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateSpriteRendererOpacity(SpriteRenderer spriteRenderer, float startOpacity, float endOpacity, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            Color color = spriteRenderer.color;

            AnimateValue<float>(startOpacity, endOpacity, duration, (a, b, t) => Mathf.Lerp(a, b, t), value => spriteRenderer.color = new Color(color.r, color.g, color.b, value), useRealtime, animationCurve);
        }

        /// <summary>
        /// Animates an objects <see cref="Image"/> component alpha from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateImageOpacity(Image image, float startOpacity, float endOpacity, float duration, bool useRealtime = false, AnimationCurve animationCurve = default)
        {
            if (animationCurve == default) animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

            Color color = image.color;

            AnimateValue<float>(startOpacity, endOpacity, duration, (a, b, t) => Mathf.Lerp(a, b, t), value => image.color = new Color(color.r, color.g, color.b, value), useRealtime, animationCurve);
        }
        #endregion

        #region Audio
        /// <summary>
        /// Animates an objects <see cref="AudioSource"/> component volume from a starting value to an ending value over a specified duration.
        /// </summary>
        public static void AnimateAudioVolume(AudioSource audioSource, float startVolume, float endVolume, float duration, bool useRealtime = true)
        {
            AnimateValue<float>(startVolume, endVolume, duration, (a, b, t) => Mathf.Lerp(a, b, t), value => audioSource.volume = value, useRealtime, AnimationCurve.Linear(0, 0, 1, 1));
        }

        /// <summary>
        /// Animates an objects <see cref="AudioSource"/> component volume from its current volume to 0.
        /// </summary>
        public static void FadeOutAudio(AudioSource audioSource, float duration, bool useRealtime = true)
        {
            AnimateAudioVolume(audioSource, audioSource.volume, 0, duration, useRealtime);
        }

        /// <summary>
        /// Animates an objects <see cref="AudioSource"/> component volume from 0 to a specified volume.
        /// </summary>
        public static void FadeInAudio(AudioSource audioSource, float duration, float endVolume = 1, bool useRealtime = true)
        {
            AnimateAudioVolume(audioSource, 0, endVolume, duration, useRealtime);
        }
        #endregion  


        /// <summary>
        /// Flips the <see cref="inputBool"/> after the specified amount of time
        /// </summary>
        /// <param name="inputBool">Bool to flip.</param>
        /// <param name="time">Time to wait.</param>
        /// <param name="useRealtime">true to use unscaled real time for the bool (ignoring time scale)</param>
        public static void FlipBoolAfterTime(bool inputBool, float time, bool useRealtime = false)
        {
            ChangeValueAfterTime<bool>(!inputBool, time, value => inputBool = value, useRealtime);
        }

        /// <summary>
        /// Destroys an object after the given amount of time in unscaled time
        /// </summary>
        public static void DestroyUnscaledtime(GameObject obj, float time)
        {
            ChangeValueAfterTime<float>(0, time, value => UnityEngine.Object.Destroy(obj), true);
        }

        /// <summary>
        /// Animates a value from a starting value to an ending value over a specified duration
        /// </summary>
        /// <param name="lerpFunction">Decides how the start and end are lerped. Use things like Mathf.Lerp() or Vector3.Lerp() for different types of values</param>
        /// <param name="onValueChanged">A callback that is invoked with the current interpolated Vector3 value as the animation progresses.</param>
        /// <param name="useRealtime">true to use unscaled real time for the animation (ignoring time scale)</param>
        /// <param name="animationCurve">Default is a linear curve</param>
        public static void AnimateValue<T>(T start, T end, float duration, Func<T, T, float, T> lerpFunction, Action<T> onValueChanged, bool useRealtime = false, AnimationCurve curve = default)
        {
            if (curve == default) curve = AnimationCurve.Linear(0, 0, 1, 1);
            CoroutineStarter.Starter.StartCoroutine(AnimateValueCoroutine(start, end, curve, duration, lerpFunction, onValueChanged, useRealtime));
        }

        /// <summary>
        /// Changes a specified value after a certain amount of time
        /// </summary>
        /// <param name="onValueChanged">A callback that is invoked with the current interpolated Vector3 value as the animation progresses.</param>
        /// <param name="useRealtime">true to use unscaled real time for the value (ignoring time scale)</param>
        public static void ChangeValueAfterTime<T>(T updatedValue, float time, Action<T> onValueChange, bool useRealtime = false)
        {
            CoroutineStarter.Starter.StartCoroutine(ChangeValueAfterTimeCoroutine(onValueChange, updatedValue, time, useRealtime));
        }

        private static IEnumerator AnimateValueCoroutine<T>(T start, T end, AnimationCurve curve, float duration, Func<T, T, float, T> lerpFunction, Action<T> onValueChanged, bool useRealtime = false)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += useRealtime ? Time.unscaledDeltaTime : Time.deltaTime;

                float time = Mathf.Clamp01(elapsed / duration);

                T value = lerpFunction(start, end, curve.Evaluate(time));

                onValueChanged?.Invoke(value);

                yield return null;
            }

            // Ensure final value
            onValueChanged?.Invoke(end);
        }

        private static IEnumerator ChangeValueAfterTimeCoroutine<T>(Action<T> onValueChange, T updatedValue, float time, bool useRealtime)
        {
            yield return new WaitForSeconds(useRealtime ? Time.unscaledDeltaTime : Time.deltaTime);
            onValueChange?.Invoke(updatedValue);
        }
    }
}