using System.Runtime.CompilerServices;

namespace Orbit.Input;

public partial class GameController
{
    public Stick Dpad { get; }
    
    public Stick LeftStick { get; }

    public Stick RightStick { get; }

    private bool buttonNorth;

    public bool ButtonNorth
    {
        get => buttonNorth;
        set => SetState(ref this.buttonNorth, value);
    }
    
    private bool buttonSouth;

    public bool ButtonSouth
    {
        get => buttonSouth;
        set => SetState(ref this.buttonSouth, value);
    }
    
    private bool buttonEast;

    public bool ButtonEast
    {
        get => buttonEast;
        set => SetState(ref this.buttonEast, value);
    }
    
    private bool buttonWest;

    public bool ButtonWest
    {
        get => buttonWest;
        set => SetState(ref this.buttonWest, value);
    }
    
    private bool pause;

    public bool Pause
    {
        get => pause;
        set => SetState(ref this.pause, value);
    }
    
    public Shoulder LeftShoulder { get; }
    
    public Shoulder RightShoulder { get; }
    
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
    
    private void SetState(ref bool field, bool newValue, [CallerMemberName] string? buttonName = null)
    {
        ArgumentNullException.ThrowIfNull(buttonName);
        
        field = newValue;
        
        this.RaiseButtonPressed(buttonName, field);
    }
}
