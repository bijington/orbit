using Windows.Gaming.Input;

namespace Orbit.Input;

public partial class GameControllerManager
{
    private GameControllerManager()
    {
    }
    
    public partial Task Initialize()
    {
        var gamePads = Gamepad.Gamepads;

        foreach (var gamePad in gamePads)
        {    
            gameControllers.Add(new GameController(gamePad));
        }

        return Task.CompletedTask;
    }
}