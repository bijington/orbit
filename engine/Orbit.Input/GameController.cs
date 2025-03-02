namespace Orbit.Input;

public partial class GameController
{
    public event EventHandler<GameControllerButtonChangedEventArgs>? ButtonChanged;
    public event EventHandler<GameControllerValueChangedEventArgs>? ValueChanged;
    
    public Stick Dpad { get; }
    
    public Stick LeftStick { get; }

    public Stick RightStick { get; }
    
    public ButtonValue<bool> East { get; }
    
    public ButtonValue<bool> North { get; }
    
    public ButtonValue<bool> South { get; }
    
    public ButtonValue<bool> West { get; }
    
    public ButtonValue<bool> Pause { get; }
    
    public Shoulder LeftShoulder { get; }
    
    public Shoulder RightShoulder { get; }
    
    internal void RaiseButtonValueChanged(ButtonValue buttonValue)
    {
        if (buttonValue is ButtonValue<float> floatValue)
        {
            this.ValueChanged?.Invoke(this, new GameControllerValueChangedEventArgs(buttonValue.Name, floatValue.Value));   
        }
        else if (buttonValue is ButtonValue<bool> boolValue)
        {
            this.ButtonChanged?.Invoke(this, new GameControllerButtonChangedEventArgs(buttonValue.Name, boolValue.Value));   
        }
    }
}

public class GameControllerButtonChangedEventArgs : EventArgs
{
    public string ButtonName { get; }
    public bool IsPressed { get; }

    public GameControllerButtonChangedEventArgs(string buttonName, bool isPressed)
    {
        ButtonName = buttonName;
        IsPressed = isPressed;
    }
}

public class GameControllerValueChangedEventArgs : EventArgs
{
    public string ButtonName { get; }
    public float Value { get; }

    public GameControllerValueChangedEventArgs(string buttonName, float value)
    {
        ButtonName = buttonName;
        Value = value;
    }
}

internal static class NameHelper
{
    internal static string GetName(string parent, string child)
    {
        if (string.IsNullOrEmpty(parent))
        {
            return child;
        }
        
        return parent + "." + child;
    }
}