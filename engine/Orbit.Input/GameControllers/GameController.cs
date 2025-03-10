﻿namespace Orbit.Input;

/// <summary>
/// Represents a physical game controller that is connected to a device.
/// </summary>
public partial class GameController
{
    private readonly WeakEventManager weakEventManager = new();
    
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
    /// Event that is raised when a button on the game controller is detected as being pressed or released.
    /// </summary>
    public event EventHandler<GameControllerButtonChangedEventArgs> ButtonChanged
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }
    
    /// <summary>
    /// Event that is raised when a button that supports a varying value on the game controller is detected as being pressed or released to some degree.
    /// </summary>
    public event EventHandler<GameControllerValueChangedEventArgs> ValueChanged
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }
    
    internal void RaiseButtonValueChanged(ButtonValue buttonValue)
    {
        switch (buttonValue)
        {
            case ButtonValue<float> floatValue:
                weakEventManager.HandleEvent(this, new GameControllerValueChangedEventArgs(buttonValue.Name, floatValue.Value), nameof(ValueChanged));
                break;
            case ButtonValue<bool> boolValue:
                weakEventManager.HandleEvent(this, new GameControllerButtonChangedEventArgs(buttonValue.Name, boolValue.Value), nameof(ButtonChanged));
                break;
        }
    }
}
