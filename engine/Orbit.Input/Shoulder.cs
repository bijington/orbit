namespace Orbit.Input;

public class Shoulder
{
    public Shoulder(GameController controller, string name)
    {
        Button = new ButtonValue<bool>(controller, name, nameof(Button));
        Trigger = new ButtonValue<float>(controller, name, nameof(Trigger));
    }

    public ButtonValue<bool> Button { get; }
    public ButtonValue<float> Trigger { get; }
}
