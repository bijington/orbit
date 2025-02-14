namespace Orbit.Input;

public partial class GameControllerManager
{
    private List<GameController> gameControllers = [];
    
    private static GameControllerManager? current;
    
    public static GameControllerManager Current => current ?? (current = new GameControllerManager());
    
    public partial Task Initialize();
    
    public IReadOnlyCollection<GameController> GameControllers => gameControllers;
    
    public event EventHandler<GameControllerConnectedEventArgs>? GameControllerConnected;

    private void OnGameControllerConnected(GameController controller)
    {
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