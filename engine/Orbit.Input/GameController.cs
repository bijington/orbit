namespace Orbit.Input;

public partial class GameController
{
    public partial Task Initialise();

    public Stick Dpad { get; } = new();
    public Stick LeftStick { get; } = new();

    public Stick RightStick { get; } = new();
    
    public bool Pause { get; set; }
    
    public bool ButtonNorth { get; set; }
    
    public bool ButtonSouth { get; set; }
    
    public bool ButtonEast { get; set; }
    
    public bool ButtonWest { get; set; }
    
    public Shoulder LeftShoulder { get; } = new();
    
    public Shoulder RightShoulder { get; } = new();
    
    public GameController When(string button, Action<bool> isPressed)
    {
        if (buttonPressedCallbacks.TryGetValue(button, out var callbacks) is false)
        {
            callbacks = [isPressed];
        }
        else
        {
            callbacks.Add(isPressed);
        }
        
        buttonPressedCallbacks[button] = callbacks;
        return this;
    }
    
    public GameController When(string button, Action<float> changesValue)
    {
        if (buttonValueChangeCallbacks.TryGetValue(button, out var callbacks) is false)
        {
            callbacks = [changesValue];
        }
        else
        {
            callbacks.Add(changesValue);
        }
        
        buttonValueChangeCallbacks[button] = callbacks;
        return this;
    }
    
    private readonly IDictionary<string, IList<Action<bool>>> buttonPressedCallbacks = new Dictionary<string, IList<Action<bool>>>();
    private readonly IDictionary<string, IList<Action<float>>> buttonValueChangeCallbacks = new Dictionary<string, IList<Action<float>>>();

    internal void RaiseButtonPressed(string button, bool isPressed)
    {
        if (buttonPressedCallbacks.TryGetValue(button, out var callbacks) is false)
        {
            return;
        }
        
        foreach (var callback in callbacks)
        {
            callback.Invoke(isPressed);
        }
    }
    
    internal void RaiseButtonValueChanged(string button, float value)
    {
        if (buttonValueChangeCallbacks.TryGetValue(button, out var callbacks) is false)
        {
            return;
        }
        
        foreach (var callback in callbacks)
        {
            callback.Invoke(value);
        }
    }
}

public class Shoulder
{
    public bool Button { get; set; }
    
    public float Trigger { get; set; }
}

public class Stick
{
    public float XAxis { get; set; }
    
    public float YAxis { get; set; }
}