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
        RawGameController.RawGameControllerAdded += OnRawGameControllerAdded;

        return Task.CompletedTask;
    }

    private void OnRawGameControllerAdded(object? sender, RawGameController rawGameController)
    {
        var barry = new bool[rawGameController.ButtonCount];
        var sarry = new GameControllerSwitchPosition[rawGameController.SwitchCount];
        var aarry = new double[rawGameController.AxisCount];
        rawGameController.GetCurrentReading(barry, sarry, aarry);

        //var controller = new GameController(rawGameController);
        //gameControllers.Add(controller);
        //GameControllerConnected?.Invoke(this, new GameControllerConnectedEventArgs(controller));
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