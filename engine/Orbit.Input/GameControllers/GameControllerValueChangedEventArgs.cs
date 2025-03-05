namespace Orbit.Input;

/// <summary>
/// Contains event data for all button value changed events.
/// </summary>
public class GameControllerValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the name of the button.
    /// </summary>
    public string ButtonName { get; }
    
    /// <summary>
    /// Gets how much the button has been pressed.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// Creates a new instance of <see cref="GameControllerValueChangedEventArgs"/>.
    /// </summary>
    /// <param name="buttonName">The name of the button.</param>
    /// <param name="value">How much the button has been pressed.</param>
    public GameControllerValueChangedEventArgs(string buttonName, float value)
    {
        ButtonName = buttonName;
        Value = value;
    }
}
