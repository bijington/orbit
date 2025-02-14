namespace Orbit.Input;

public class Shoulder : ButtonElement
{
    public Shoulder(GameController controller, string name) : base(controller, name)
    {
    }

    private bool button;

    public bool Button
    {
        get => button;
        set => SetState(ref this.button, value);
    }
    
    private float trigger;

    public float Trigger
    {
        get => trigger;
        set => SetValue(ref this.trigger, value);
    }
}
