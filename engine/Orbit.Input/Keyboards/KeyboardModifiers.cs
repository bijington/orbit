namespace Orbit.Input;

#pragma warning disable CS1591 // Let's be pragmatic here... most of these are self explanatory.
[Flags]
public enum KeyboardModifiers
{
    None = 0,
    
    ShiftLeft = 1,
    
    ShiftRight = 2,
    
    Shift = ShiftLeft | ShiftRight,
    
    AltLeft = 4,
    
    AltRight = 8,
    
    Alt = AltLeft | AltRight,

    ControlLeft = 16,
    
    ControlRight = 32,
    
    Control = ControlLeft | ControlRight
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member