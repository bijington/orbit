namespace Orbit.Input;

public partial class GameControllerOptions
{
    public bool StartControllerMonitoringUponDetection { get; set; } = true;

    public TimeSpan ControllerUpdateFrequency { get; set; } = TimeSpan.FromMicroseconds(100);
}
