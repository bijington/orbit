namespace Orbit.Input;

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
