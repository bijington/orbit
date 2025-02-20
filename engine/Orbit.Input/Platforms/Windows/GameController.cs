using Windows.Gaming.Input;

namespace Orbit.Input;

public partial class GameController
{
    private readonly Gamepad gamepad;
    private CancellationTokenSource? cancellationTokenSource;

    public GameController(Gamepad gamepad)
    {
        this.gamepad = gamepad;

        Dpad = new Stick(this, nameof(Dpad));
        LeftStick = new Stick(this, nameof(LeftStick));
        RightStick = new Stick(this, nameof(RightStick));

        LeftShoulder = new Shoulder(this, nameof(LeftShoulder));
        RightShoulder = new Shoulder(this, nameof(RightShoulder));
    }

    public void StartUpdates(TimeSpan updateFrequency)
    {
        if (this.cancellationTokenSource is not null)
        {
            return;
        }

        this.cancellationTokenSource = new();

        Task.Run(async () =>
        {
            while (this.cancellationTokenSource?.IsCancellationRequested is false)
            {
                Update();

                await Task.Delay(updateFrequency);
            }
        });
    }

    public void StopUpdates()
    {
        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
    }

    private void Update()
    {
        var reading = gamepad.GetCurrentReading();

        LeftStick.XAxis = (float)reading.LeftThumbstickX;
        LeftStick.YAxis = (float)reading.LeftThumbstickY;

        RightStick.XAxis = (float)reading.RightThumbstickX;
        RightStick.YAxis = (float)reading.RightThumbstickY;

        LeftShoulder.Button = reading.Buttons.HasFlag(GamepadButtons.LeftShoulder);
        LeftShoulder.Trigger = (float)reading.LeftTrigger;

        RightShoulder.Button = reading.Buttons.HasFlag(GamepadButtons.RightShoulder);
        RightShoulder.Trigger = (float)reading.RightTrigger;

        ButtonNorth = reading.Buttons.HasFlag(GamepadButtons.Y);
        ButtonEast = reading.Buttons.HasFlag(GamepadButtons.B);
        ButtonSouth = reading.Buttons.HasFlag(GamepadButtons.A);
        ButtonWest = reading.Buttons.HasFlag(GamepadButtons.X);

        Dpad.XAxis = reading.Buttons.HasFlag(GamepadButtons.DPadRight) ? 1f : reading.Buttons.HasFlag(GamepadButtons.DPadLeft) ? -1f : 0f;
        Dpad.YAxis = reading.Buttons.HasFlag(GamepadButtons.DPadDown) ? 1f : reading.Buttons.HasFlag(GamepadButtons.DPadUp) ? -1f : 0f;
    }
}