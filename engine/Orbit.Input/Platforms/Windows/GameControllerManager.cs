using Windows.Gaming.Input;

namespace Orbit.Input;

public partial class GameControllerManager
{
    private GameControllerManager()
    {
    }

    public bool StartControllerMonitoringUponDetection { get; set; } = true;

    public TimeSpan ControllerUpdateFrequency { get; set; } = TimeSpan.FromMicroseconds(100);

    public partial Task StartDiscovery()
    {
        Gamepad.GamepadAdded += OnGamepadAdded;

        return Task.CompletedTask;
    }

    private void OnGamepadAdded(object? sender, Gamepad gamepad)
    {
        var controller = new GameController(gamepad);
        OnGameControllerConnected(controller);

        if (StartControllerMonitoringUponDetection)
        {
            controller.StartUpdates(ControllerUpdateFrequency);
        }
    }
}