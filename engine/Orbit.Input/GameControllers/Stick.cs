namespace Orbit.Input;

/// <summary>
/// Definition of a multi-axis component on a game pad. This could be a joystick, thumbstick or d-pad.
/// </summary>
public class Stick
{
    /// <summary>
    /// Creates
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="name"></param>
    public Stick(GameController controller, string name)
    {
        XAxis = new ButtonValue<float>(controller, name, nameof(XAxis), FloatComparer.Default);
        YAxis = new ButtonValue<float>(controller, name, nameof(YAxis), FloatComparer.Default);
    }

    public ButtonValue<float> XAxis { get; }
    public ButtonValue<float> YAxis { get; }
}
