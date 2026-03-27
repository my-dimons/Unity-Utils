using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects.ColorFlash {
  [RequireComponent(typeof(SpriteRenderer))]
  public class ColorFlashManager : MonoBehaviour {
    /// <summary>
    /// The default <see cref="ColorFlash"/> to use when flashing.
    /// </summary>
    public ColorFlash colorFlash;

    [Header("Debug Logs")]

    /// <summary>
    /// If true, will Debug.Log the color and duration when flashing.
    /// </summary>
    public bool logFlash = false;

    private const string SPRITE_MATERIAL_PATH = "Materials/ColorFlash/ColorFlash-Lit-Sprite-MAT";

    private static Material spriteRendererFlashMaterial;

    /// <summary>
    /// Set on start as the sprite renderer's material. Can be changed by calling <see cref="SetOriginalMaterial(Material)"/>.
    /// </summary>
    private Material originalMaterial;

    private SpriteRenderer spriteRenderer;

    private Coroutine flashRoutine;

    void Start() {
      spriteRenderer = GetComponent<SpriteRenderer>();
      originalMaterial = spriteRenderer.material;

      spriteRendererFlashMaterial = GetMaterialInstance(Resources.Load<Material>(SPRITE_MATERIAL_PATH));
    }

    /// <summary>
    /// Determines whether a flashing operation is currently active.
    /// </summary>
    /// <returns>true if the object is flashing; otherwise, false.</returns>
    public bool IsFlashing() {
      return flashRoutine != null;
    }

    /// <summary>
    /// Sets the material that is switched back to after a flash is finished.
    /// </summary>
    /// <param name="mat">Material to switch <see cref="originalMaterial"/> to</param>
    public void SetOriginalMaterial(Material mat) {
      originalMaterial = mat;
    }

    /// <summary>
    /// Flashes the object using the provided <see cref="ColorFlash"/> variables. If a flash is already active, a warning is logged and no new flash is started.
    /// </summary>
    /// <param name="colorFlash">The color flash to use when flashing</param>
    /// <returns>The coroutine started by the flash</returns>
    public Coroutine Flash(ColorFlash colorFlash) {
      if (IsFlashing()) {
        Debug.LogWarning("Object is already flashing, no flash was started");
        return null;
      } else {
        return StartCoroutine(FlashRoutine(colorFlash));
      }
    }

    /// <summary>
    /// Calls <see cref="Flash(ColorFlash)"/> using the default <see cref="colorFlash"/> variables.
    /// </summary>
    /// <returns>The coroutine started by the flash</returns>
    [ContextMenu("Flash")]
    public Coroutine Flash() {
      return Flash(colorFlash);
    }

    private IEnumerator FlashRoutine(ColorFlash flash) {
      spriteRenderer.material = spriteRendererFlashMaterial;
      spriteRendererFlashMaterial.SetColor("_FlashColor", flash.color);
      spriteRendererFlashMaterial.SetFloat("_FlashAmount", flash.amount);

      if (logFlash)
        Debug.Log("Flashing object " +
          "\n color: " + flash.color +
          "\n duration: " + flash.duration);

      yield return FlashFade(flash.fadeInTime, flash.fadeInCurve, false);

      yield return new WaitForSeconds(flash.duration);

      yield return FlashFade(flash.fadeOutTime, flash.fadeOutCurve, true);

      if (logFlash)
        Debug.Log("Finished flashing object");

      spriteRenderer.material = originalMaterial;

      flashRoutine = null;
    }

    /// <summary>
    /// Fades in/out the flash effect by animating the "_FlashAmount" property of the flash material over time using the provided animation curve.
    /// </summary>
    private IEnumerator FlashFade(float fadeTime, AnimationCurve curve, bool fadeOut) {
      float elapsedTime = 0f;

      while (elapsedTime < fadeTime) {

        // iterate elapsedTime
        elapsedTime += colorFlash.useRealtime ? Time.unscaledDeltaTime : Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / fadeTime);
        float curveValue = curve.Evaluate(t);

        if (fadeOut)
          curveValue = 1f - curveValue;

        // lerp the flash amount
        float currentFlashAmount = colorFlash.amount * (curveValue);
        spriteRendererFlashMaterial.SetFloat("_FlashAmount", currentFlashAmount);

        yield return null;
      }
    }

    /// <summary>
    /// Creates a new instance of the specified material, or returns null if the input is null.
    /// </summary>
    /// <remarks>A warning is logged if the input material is null. The returned material is a separate
    /// instance and changes to it do not affect the original material.</remarks>
    /// <param name="material">The material to duplicate</param>
    /// <returns>A new instance of the specified material, or null if the input material is null.</returns>
    private static Material GetMaterialInstance(Material material) {
      if (material != null)
        return new Material(material);
      else {
        Debug.LogWarning("Unable to get material instance");
        return null;
      }
    }
  }
}