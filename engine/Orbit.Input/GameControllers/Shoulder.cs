namespace Orbit.Input;

/// <summary>
/// Represents a collection of buttons on ther 'shoulder' of a game controller.
/// </summary>
public class Shoulder
{
    /// <summary>
    /// Creates a new instance of <see cref="Shoulder"/>.
    /// </summary>
    /// <param name="controller">The <see cref="GameController"/> that the shoulder belongs to.</param>
    /// <param name="name">The name of the shoulder (e.g. left or right).</param>
    public Shoulder(GameController controller, string name)
    {
        Button = new ButtonValue<bool>(controller, name, nameof(Button));
        Trigger = new ButtonValue<float>(controller, name, nameof(Trigger), FloatComparer.Default);
    }

    /// <summary>
    /// Gets the <see cref="ButtonValue{TValue}"/> that represents the top button on the shoulder.
    /// This is represented as a simple pressed/released button.
    /// </summary>
    public ButtonValue<bool> Button { get; }
    
    /// <summary>
    /// Gets the <see cref="ButtonValue{TValue}"/> that represents the bottom button on the shoulder.
    /// This is represented as a value between 0 and 1 based on how much the button is pressed.
    /// </summary>
    public ButtonValue<float> Trigger { get; }
}
