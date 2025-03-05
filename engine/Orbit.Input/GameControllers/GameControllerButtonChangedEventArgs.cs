namespace Orbit.Input;

/// <summary>
/// Contains event data for all button pressed and released events.
/// </summary>
public class GameControllerButtonChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name of the button.
    /// </summary>
    public string ButtonName { get; }
    
    /// <summary>
    /// Gets whether the button is pressed or released.
    /// </summary>
    public bool IsPressed { get; }

    /// <summary>
    /// Creates a new instance of <see cref="GameControllerButtonChangedEventArgs"/>.
    /// </summary>
    /// <param name="buttonName">The name of the button.</param>
    /// <param name="isPressed">Whether the button is pressed or released.</param>
    public GameControllerButtonChangedEventArgs(string buttonName, bool isPressed)
    {
        ButtonName = buttonName;
        IsPressed = isPressed;
    }
}
