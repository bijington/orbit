using Microsoft.UI.Xaml.Controls.Primitives;

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

        North = new ButtonValue<bool>(this, nameof(North));
        South = new ButtonValue<bool>(this, nameof(South));
        East = new ButtonValue<bool>(this, nameof(East));
        West = new ButtonValue<bool>(this, nameof(West));
        Pause = new ButtonValue<bool>(this, nameof(Pause));
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

        LeftStick.XAxis.Value = (float)reading.LeftThumbstickX;
        LeftStick.YAxis.Value = (float)reading.LeftThumbstickY;

        RightStick.XAxis.Value = (float)reading.RightThumbstickX;
        RightStick.YAxis.Value = (float)reading.RightThumbstickY;

        LeftShoulder.Button.Value = reading.Buttons.HasFlag(GamepadButtons.LeftShoulder);
        LeftShoulder.Trigger.Value = (float)reading.LeftTrigger;

        RightShoulder.Button.Value = reading.Buttons.HasFlag(GamepadButtons.RightShoulder);
        RightShoulder.Trigger.Value = (float)reading.RightTrigger;

        North.Value = reading.Buttons.HasFlag(GamepadButtons.Y);
        East.Value = reading.Buttons.HasFlag(GamepadButtons.B);
        South.Value = reading.Buttons.HasFlag(GamepadButtons.A);
        West.Value = reading.Buttons.HasFlag(GamepadButtons.X);

        Dpad.XAxis.Value = reading.Buttons.HasFlag(GamepadButtons.DPadRight) ? 1f : reading.Buttons.HasFlag(GamepadButtons.DPadLeft) ? -1f : 0f;
        Dpad.YAxis.Value = reading.Buttons.HasFlag(GamepadButtons.DPadDown) ? 1f : reading.Buttons.HasFlag(GamepadButtons.DPadUp) ? -1f : 0f;
    }
}