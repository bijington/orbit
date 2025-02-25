using System.Collections.Concurrent;

namespace Orbit.Input;

public partial class KeyboardManager
{
    private static KeyboardManager? current;
    private ConcurrentDictionary<KeyboardKey, bool> pressedKeys;
    private readonly IDictionary<KeyboardKey, IList<Action<bool>>> keyPressedCallbacks = new Dictionary<KeyboardKey, IList<Action<bool>>>();

    private KeyboardManager()
    {
        pressedKeys = new ConcurrentDictionary<KeyboardKey, bool>(
            Enum.GetValues(typeof(KeyboardKey)).Cast<KeyboardKey>().ToDictionary(k => k, k => false));
    }
    
    public bool this[KeyboardKey key] => pressedKeys.TryGetValue(key, out var value) && value;
    
    public static KeyboardManager Current => current ??= new KeyboardManager();
    
    public event EventHandler<KeyboardKey>? KeyDown;

    public event EventHandler<KeyboardKey>? KeyUp;

    private void KeyboardKeyPressed(KeyboardKey key)
    {
        CheckAndApplyModifiers(key, true);
        
        pressedKeys.TryUpdate(key, true, false);
        
        KeyDown?.Invoke(null, key);
    }

    private void KeyboardKeyReleased(KeyboardKey key)
    {
        CheckAndApplyModifiers(key, false);
        
        pressedKeys.TryUpdate(key, false, true);
        
        KeyUp?.Invoke(null, key);
    }

    public bool IsShiftPressed
    {
        get
        {
            return IsLeftShiftDown || IsRightShiftDown;
        }
    }

    public bool IsAltPressed
    {
        get
        {
            return IsLeftAltDown || IsRightAltDown;
        }
    }

    public bool IsControlPressed
    {
        get
        {
            return IsLeftControlDown || IsRightControlDown;
        }
    }

    static bool IsLeftShiftDown { get; set; }

    static bool IsRightShiftDown { get; set; }

    static bool IsLeftAltDown { get; set; }

    static bool IsRightAltDown { get; set; }

    static bool IsLeftControlDown { get; set; }

    static bool IsRightControlDown { get; set; }
    
    public KeyboardManager When(KeyboardKey key, Action<bool> isPressed)
    {
        if (keyPressedCallbacks.TryGetValue(key, out var callbacks) is false)
        {
            callbacks = [isPressed];
        }
        else
        {
            callbacks.Add(isPressed);
        }
        
        keyPressedCallbacks[key] = callbacks;
        return this;
    }

    static void CheckAndApplyModifiers(KeyboardKey key, bool state)
    {
        if (key == KeyboardKey.ShiftLeft)
        {
            IsLeftShiftDown = state;
        }
        else
        if (key == KeyboardKey.ShiftRight)
        {
            IsRightShiftDown = state;
        }
        else
        if (key == KeyboardKey.AltLeft)
        {
            IsLeftAltDown = state;
        }
        else
        if (key == KeyboardKey.AltRight)
        {
            IsRightAltDown = state;
        }
        else
        if (key == KeyboardKey.ControlLeft)
        {
            IsLeftControlDown = state;
        }
        else
        if (key == KeyboardKey.ControlRight)
        {
            IsRightControlDown = state;
        }
    }
}