namespace Orbit.Input;

public class Stick
{
    public Stick(GameController controller, string name)
    {
        XAxis = new ButtonValue<float>(controller, name, nameof(XAxis), FloatComparer.Default);
        YAxis = new ButtonValue<float>(controller, name, nameof(YAxis), FloatComparer.Default);
    }

    public ButtonValue<float> XAxis { get; }
    public ButtonValue<float> YAxis { get; }
}
