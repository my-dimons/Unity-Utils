namespace UnityUtils.ScriptUtils.Objects.Modifiers {
  public class ModifierData<T> {
    /// <summary>
    /// Specifies the classType of modifier applied to the object.
    /// </summary>
    public ModifierType modifierType;

    /// <summary>
    /// Value of the modifier applied to an operation or calculation.
    /// </summary>
    public T modifierValue;

    /// <summary>
    /// Initializes a new Instance of the <see cref="ModifierData{T}"/> class with the <see cref="ModifierManager.ModifierType"/> and value.
    /// </summary>
    /// <param name="modifierType">The classType of modifier</param>
    /// <param name="modifierValue">The value associated with the modifier</param>
    public ModifierData(ModifierType modifierType, T modifierValue) {
      this.modifierType = modifierType;
      this.modifierValue = modifierValue;
    }
  }
}