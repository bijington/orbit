namespace Orbit.Input;

public class Stick
{
    public Stick(GameController controller, string name)
    {
        XAxis = new ButtonValue<float>(controller, name, nameof(XAxis));
        YAxis = new ButtonValue<float>(controller, name, nameof(YAxis));
    }

    public ButtonValue<float> XAxis { get; }
    public ButtonValue<float> YAxis { get; }
}
