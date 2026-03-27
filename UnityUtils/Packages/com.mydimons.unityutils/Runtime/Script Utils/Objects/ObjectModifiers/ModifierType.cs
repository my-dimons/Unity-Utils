namespace UnityUtils.ScriptUtils.Objects.Modifiers {
  /// <summary>
  /// Enum with different modifier types that get used in <see cref="ModifierManager{T}"/> when applying modifiers to an input value.
  /// </summary>
  public enum ModifierType {
    /// <summary>
    /// Addition/Subtraction modifier
    /// </summary>
    Flat,
    /// <summary>
    /// Multiplication modifier
    /// </summary>
    Multiply,
    /// <summary>
    /// Division modifier, divides the input value by the modifier value. If value is 0, it will be ignored to avoid dividing by zero errors.
    /// </summary>
    Divide,
    /// <summary>
    /// Root Modifier, gets the root of the input value based on the modifier value. 
    /// </summary>
    Root,
    /// <summary>
    /// Exponent Modifier, raises the input value to the power of the modifier value.
    /// </summary>
    Exponent
  }
}