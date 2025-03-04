namespace Orbit.Input;

public class GameControllerConnectedEventArgs : EventArgs
{
    public GameControllerConnectedEventArgs(GameController gameController)
    {
        GameController = gameController;
    }
    
    public GameController GameController { get; }
}
