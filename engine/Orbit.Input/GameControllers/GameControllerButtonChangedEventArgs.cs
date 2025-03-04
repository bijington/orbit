namespace Orbit.Input;

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
