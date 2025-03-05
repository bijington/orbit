namespace Orbit.Input;

/// <summary>
/// Definition of a multi-axis component on a game pad. This could be a joystick, thumbstick or d-pad.
/// </summary>
public class Stick
{
    /// <summary>
    /// Creates a new instance of <see cref="Stick"/>.
    /// </summary>
    /// <param name="controller">The <see cref="GameController"/> that the stick belongs to.</param>
    /// <param name="name">The name of the stick (e.g. left or right).</param>
    public Stick(GameController controller, string name)
    {
        XAxis = new ButtonValue<float>(controller, name, nameof(XAxis), FloatComparer.Default);
        YAxis = new ButtonValue<float>(controller, name, nameof(YAxis), FloatComparer.Default);
    }
    
    /// <summary>
    /// Gets the x-axis value.
    /// This is represented as a value between -1 (fully pressed to the left) and 1 (fully pressed to the right).
    /// </summary>
    public ButtonValue<float> XAxis { get; }
    
    /// <summary>
    /// Gets the y-axis value.
    /// This is represented as a value between -1 (fully pressed to the top) and 1 (fully pressed to the bottom).
    /// </summary>
    public ButtonValue<float> YAxis { get; }
}
