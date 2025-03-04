using System.Collections.Concurrent;

namespace Orbit.Input;

/// <summary>
/// Provides the ability to interact with a keyboard connected to a device.
/// </summary>
public partial class KeyboardManager
{
    private static KeyboardManager? current;
    private readonly ConcurrentDictionary<KeyboardKey, bool> pressedKeys;
    private readonly IReadOnlyDictionary<KeyboardKey, KeyboardModifiers> modifierKeys;
    private readonly WeakEventManager weakEventManager = new();

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
    
    /// <summary>
    /// Provides the ability to check the state of the supplied <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The <see cref="KeyboardKey"/> to check the pressed state for.</param>
    public bool this[KeyboardKey key] => pressedKeys.TryGetValue(key, out var value) && value;
    
    /// <summary>
    /// Gets the current instance of the <see cref="KeyboardManager"/>.
    /// </summary>
    public static KeyboardManager Current => current ??= new KeyboardManager();
    
    /// <summary>
    /// Gets whether any <see cref="KeyboardModifiers"/> are currently pressed.
    /// </summary>
    public KeyboardModifiers Modifiers { get; private set; }
    
    /// <summary>
    /// Event that is raised when a <see cref="KeyboardKey"/> is detected as being pressed.
    /// </summary>
    public event EventHandler<KeyboardKeyChangeEventArgs> KeyDown
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }

    /// <summary>
    /// Event that is raised when a <see cref="KeyboardKey"/> is detected as being released.
    /// </summary>
    public event EventHandler<KeyboardKeyChangeEventArgs> KeyUp
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }

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
    
    private void KeyboardKeyPressed(KeyboardKey key)
    {
        ApplyModifier(key);
        
        pressedKeys.TryUpdate(key, true, false);
        
        weakEventManager.HandleEvent(this, new KeyboardKeyChangeEventArgs(key), nameof(KeyDown));
    }

    private void KeyboardKeyReleased(KeyboardKey key)
    {
        ClearModifier(key);
        
        pressedKeys.TryUpdate(key, false, true);
        
        weakEventManager.HandleEvent(this, new KeyboardKeyChangeEventArgs(key), nameof(KeyUp));
    }
}
