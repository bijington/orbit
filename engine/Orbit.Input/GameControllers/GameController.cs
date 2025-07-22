using System.Collections.Concurrent;

namespace Orbit.Input;

/// <summary>
/// Represents a physical game controller that is connected to a device.
/// </summary>
public partial class GameController
{
    public string Name { get; private set; } = string.Empty;
    
    /// <summary>
    /// Gets the <see cref="Stick"/> that represents the D-pad on the game controller.
    /// </summary>
    public Stick Dpad { get; }
    
    /// <summary>
    /// Gets the <see cref="Stick"/> that represents the left thumbstick on the game controller.
    /// </summary>
    public Stick LeftStick { get; }

    /// <summary>
    /// Gets the <see cref="Stick"/> that represents the right thumbstick on the game controller.
    /// </summary>
    public Stick RightStick { get; }
    
    /// <summary>
    /// Gets the <see cref="ButtonValue{T}"/> that represents the right most button on the controller.
    /// Circle on Playstation and B on XBox controllers.
    /// </summary>
    public ButtonValue<bool> East { get; }
    
    /// <summary>
    /// Gets the <see cref="ButtonValue{T}"/> that represents the top most button on the controller.
    /// Triangle on Playstation and Y on XBox controllers.
    /// </summary>
    public ButtonValue<bool> North { get; }
    
    /// <summary>
    /// Gets the <see cref="ButtonValue{T}"/> that represents the bottom most button on the controller.
    /// X on Playstation and A on XBox controllers.
    /// </summary>
    public ButtonValue<bool> South { get; }
    
    /// <summary>
    /// Gets the <see cref="ButtonValue{T}"/> that represents the left most button on the controller.
    /// Square on Playstation and X on XBox controllers.
    /// </summary>
    public ButtonValue<bool> West { get; }
    
    /// <summary>
    /// Gets the <see cref="ButtonValue{T}"/> that represents the left most button on the controller.
    /// Options on Playstation and hamburger on XBox controllers.
    /// </summary>
    public ButtonValue<bool> Pause { get; }
    
    /// <summary>
    /// Gets the <see cref="Shoulder"/> that represents the left hand shoulder on the controller.
    /// </summary>
    public Shoulder LeftShoulder { get; }
    
    /// <summary>
    /// Gets the <see cref="Shoulder"/> that represents the right hand shoulder on the controller.
    /// </summary>
    public Shoulder RightShoulder { get; }

    /// <summary>
    /// U
    /// </summary>
    public IReadOnlyList<ButtonValue<bool>> UnmappedButtons => unmappedButtons.Values.ToList().AsReadOnly();
    
    private ConcurrentDictionary<string, ButtonValue<bool>> unmappedButtons = new ();

    internal void RaiseUnmappedButtonChange(string buttonName, bool isPressed)
    {
        if (unmappedButtons.TryGetValue(buttonName, out var button) is false)
        {
            button = new ButtonValue<bool>(this, buttonName);
            unmappedButtons.TryAdd(buttonName, button);
        }

        button.Value = isPressed;
    }

    /// <summary>
    /// Event that is raised when a button on the game controller is detected as being pressed or released.
    /// </summary>
    public event EventHandler<GameControllerButtonChangedEventArgs>? ButtonChanged;

    /// <summary>
    /// Event that is raised when a button that supports a varying value on the game controller is detected as being pressed or released to some degree.
    /// </summary>
    public event EventHandler<GameControllerValueChangedEventArgs>? ValueChanged;
    
    internal void RaiseButtonValueChanged(ButtonValue buttonValue)
    {
        switch (buttonValue)
        {
            case ButtonValue<float> floatValue:
                ValueChanged?.Invoke(this, new GameControllerValueChangedEventArgs(buttonValue.Name, floatValue.Value));
                break;
            case ButtonValue<bool> boolValue:
                ButtonChanged?.Invoke(this, new GameControllerButtonChangedEventArgs(buttonValue.Name, boolValue.Value));
                break;
        }
    }
}
