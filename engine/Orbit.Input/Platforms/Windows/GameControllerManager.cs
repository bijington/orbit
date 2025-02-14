using Android.Views;

namespace Orbit.Input;

public partial class GameControllerManager
{
    private GameControllerManager()
    {
    }
    
    public partial Task Initialise()
    {
        var gamePads = Gamepad.Gamepads;

        foreach (var gamePad in gamePads)
        {    
            gameControllers.Add(new GameController(gamePad));
        }

        return Task.CompletedTask;
    }
}