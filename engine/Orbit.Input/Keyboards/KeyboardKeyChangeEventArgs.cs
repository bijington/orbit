namespace Orbit.Input;

/// <summary>
/// Contains event data for all key pressed and released events.
/// </summary>
public class KeyboardKeyChangeEventArgs : EventArgs
{
    /// <summary>
    /// Gets the <see cref="KeyboardKey"/> that triggered the event.
    /// </summary>
    public KeyboardKey Key { get; }

    /// <summary>
    /// Creates a new instance of <see cref="KeyboardKeyChangeEventArgs"/>
    /// </summary>
    /// <param name="key">The <see cref="KeyboardKey"/> that triggered the event.</param>
    public KeyboardKeyChangeEventArgs(KeyboardKey key)
    {
        Key = key;
    }
}