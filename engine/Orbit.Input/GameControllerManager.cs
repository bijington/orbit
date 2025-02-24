namespace Orbit.Input;

public partial class GameControllerManager
{
    private readonly List<GameController> gameControllers = [];
    
    private static GameControllerManager? current;
    
    public static GameControllerManager Current => current ??= new GameControllerManager();
    
    public partial Task StartDiscovery();
    
    public IReadOnlyCollection<GameController> GameControllers => gameControllers;
    
    public event EventHandler<GameControllerConnectedEventArgs>? GameControllerConnected;

    private void OnGameControllerConnected(GameController controller)
    {
        gameControllers.Add(controller);
        this.GameControllerConnected?.Invoke(this, new GameControllerConnectedEventArgs(controller));
    }
}

public class GameControllerConnectedEventArgs : EventArgs
{
    public GameControllerConnectedEventArgs(GameController gameController)
    {
        GameController = gameController;
    }
    
    public GameController GameController { get; }
}