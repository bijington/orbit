using System.Collections.Concurrent;

namespace Orbit.Input;

public partial class KeyboardManager
{
    private static KeyboardManager? current;
    private ConcurrentDictionary<KeyboardKey, bool> pressedKeys;
    private readonly IReadOnlyDictionary<KeyboardKey, KeyboardModifiers> modifierKeys;

    private KeyboardManager()
    {
        pressedKeys = new ConcurrentDictionary<KeyboardKey, bool>(
            Enum.GetValues(typeof(KeyboardKey)).Cast<KeyboardKey>().ToDictionary(k => k, k => false));

        modifierKeys = new Dictionary<KeyboardKey, KeyboardModifiers>
        {
            { KeyboardKey.ShiftLeft, KeyboardModifiers.ShiftLeft },
            { KeyboardKey.ShiftRight, KeyboardModifiers.ShiftRight },
            { KeyboardKey.AltLeft, KeyboardModifiers.AltLeft },
            { KeyboardKey.AltRight, KeyboardModifiers.AltRight },
            { KeyboardKey.ControlLeft, KeyboardModifiers.ControlLeft },
            { KeyboardKey.ControlRight, KeyboardModifiers.ControlRight },
        };
    }
    
    public bool this[KeyboardKey key] => pressedKeys.TryGetValue(key, out var value) && value;
    
    public static KeyboardManager Current => current ??= new KeyboardManager();
    
    public event EventHandler<KeyboardKey>? KeyDown;

    public event EventHandler<KeyboardKey>? KeyUp;

    private void KeyboardKeyPressed(KeyboardKey key)
    {
        ApplyModifier(key);
        
        pressedKeys.TryUpdate(key, true, false);
        
        KeyDown?.Invoke(null, key);
    }

    private void KeyboardKeyReleased(KeyboardKey key)
    {
        ClearModifier(key);
        
        pressedKeys.TryUpdate(key, false, true);
        
        KeyUp?.Invoke(null, key);
    }

    public KeyboardModifiers Modifiers { get; private set; }

    private void ApplyModifier(KeyboardKey key)
    {
        if (modifierKeys.TryGetValue(key, out var modifiers))
        {
            Modifiers |= modifiers;
        }
    }
    
    private void ClearModifier(KeyboardKey key)
    {
        if (modifierKeys.TryGetValue(key, out var modifiers))
        {
            Modifiers ^= modifiers;
        }
    }
}
