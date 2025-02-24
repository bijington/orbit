using Android.App;
using Android.Views;

namespace Orbit.Input;

public partial class GameControllerManager
{
    private KeyListener? keyListener;
    private GenericMotionListener? genericMotionListener;
    
    private GameControllerManager()
    {
    }

    public void AttachToCurrentActivity(Activity activity) 
    {
        keyListener = new KeyListener(activity, (code, e) =>
        {
            switch (e.Action) 
            {
                case KeyEventActions.Down when OnKeyDown(code, e):
                case KeyEventActions.Up when OnKeyUp(code, e):
                    return true;
            }

            return false;
        });
        
        genericMotionListener = new GenericMotionListener(activity, OnGenericMotionEvent);
    }
    
    public partial Task StartDiscovery()
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
                OnGameControllerConnected(new GameController(deviceId));
            }
        }

        return Task.CompletedTask;
    }
    
    public bool OnGenericMotionEvent(MotionEvent? e)
    {
        return e is not null && gameControllers.Any(controller => controller.OnGenericMotionEvent(e));
    }

    public bool OnKeyDown(Keycode keyCode, KeyEvent? e)
    {
        return e is not null && gameControllers.Any(controller => controller.OnKeyDown(e));
    }

    public bool OnKeyUp(Keycode keyCode, KeyEvent? e)
    {
        return e is not null && gameControllers.Any(controller => controller.OnKeyUp(e));
    }
}
