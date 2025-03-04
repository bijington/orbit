namespace Orbit.Input;

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