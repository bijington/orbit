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
        switch (buttonValue)
        {
            case ButtonValue<float> floatValue:
                this.ValueChanged?.Invoke(this, new GameControllerValueChangedEventArgs(buttonValue.Name, floatValue.Value));
                break;
            case ButtonValue<bool> boolValue:
                this.ButtonChanged?.Invoke(this, new GameControllerButtonChangedEventArgs(buttonValue.Name, boolValue.Value));
                break;
        }
    }
}
