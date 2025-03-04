namespace Orbit.Input;

/// <summary>
/// Contains event data for when game controllers are detected as being connected to the device.
/// </summary>
public class GameControllerConnectedEventArgs : EventArgs
{
    /// <summary>
    /// Creates a new instance of <see cref="GameControllerConnectedEventArgs"/>.
    /// </summary>
    /// <param name="gameController">The <see cref="GameController"/> that has been detected and being connected to the device.</param>
    public GameControllerConnectedEventArgs(GameController gameController)
    {
        GameController = gameController;
    }
    
    /// <summary>
    /// Gets the <see cref="GameController"/> that has been detected and being connected to the device.
    /// </summary>
    public GameController GameController { get; }
}
