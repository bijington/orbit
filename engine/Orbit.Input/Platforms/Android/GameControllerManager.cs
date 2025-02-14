using Android.Views;

namespace Orbit.Input;

public partial class GameControllerManager
{
    private GameControllerManager()
    {
    }
    
    public partial Task Initialize()
    {
        var deviceIds = InputDevice.GetDeviceIds();

        if (deviceIds is null)
        {
            return Task.CompletedTask;
        }

        foreach (var deviceId in deviceIds)
        {
            var device = InputDevice.GetDevice(deviceId);

            if (device is null)
            {
                continue;
            }

            var sources = device.Sources;
                
            if (sources.HasFlag(InputSourceType.Gamepad) || sources.HasFlag(InputSourceType.Joystick))
            {
                gameControllers.Add(new GameController(deviceId));
            }
        }

        return Task.CompletedTask;
    }
    
    public void OnGenericMotionEvent(MotionEvent? e)
    {
        if (e is null)
        {
            return;
        }
        
        // TODO: thread safety?
        foreach (var controller in gameControllers)
        {
            controller.OnGenericMotionEvent(e);
        }
    }

    public void OnKeyDown(Keycode keyCode, KeyEvent? e)
    {
        if (e is null)
        {
            return;
        }
        
        // TODO: thread safety?
        foreach (var controller in gameControllers)
        {
            controller.OnKeyDown(e);
        }
    }

    public void OnKeyUp(Keycode keyCode, KeyEvent? e)
    {
        if (e is null)
        {
            return;
        }
        
        // TODO: thread safety?
        foreach (var controller in gameControllers)
        {
            controller.OnKeyUp(e);
        }
    }
}