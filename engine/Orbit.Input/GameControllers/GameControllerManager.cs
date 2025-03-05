namespace Orbit.Input;

/// <summary>
/// Provides the ability to interact with game controller support on each platform.
/// </summary>
public partial class GameControllerManager
{
    private readonly List<GameController> gameControllers = [];
    private readonly WeakEventManager weakEventManager = new();
    
    private static GameControllerManager? current;
    
    /// <summary>
    /// Gets the current instance of the <see cref="GameControllerManager"/>.
    /// </summary>
    public static GameControllerManager Current => current ??= new GameControllerManager();
    
    /// <summary>
    /// Starts the controller discovery process. Make sure to subscribe to the <see cref="GameControllerConnected"/> event in order to be notified when a controller has been discovered.
    /// </summary>
    /// <returns></returns>
    public partial Task StartDiscovery();
    
    /// <summary>
    /// Gets the list of <see cref="GameController"/>s that are connected to the device.
    /// </summary>
    public IReadOnlyCollection<GameController> GameControllers => gameControllers;
    
    /// <summary>
    /// Event that is raised when a <see cref="GameController"/> is detected as being connected to the device.
    /// </summary>
    public event EventHandler<GameControllerConnectedEventArgs> GameControllerConnected
    {
        add => weakEventManager.AddEventHandler(value);
        remove => weakEventManager.RemoveEventHandler(value);
    }

    private void OnGameControllerConnected(GameController controller)
    {
        gameControllers.Add(controller);
        weakEventManager.HandleEvent(this, new GameControllerConnectedEventArgs(controller), nameof(GameControllerConnected));
    }
}
