﻿using Windows.Gaming.Input;

namespace Orbit.Input;

public partial class GameController
{
    private readonly Gamepad gamepad;

    public partial Task Initialise()
    {
        var gamepad = Gamepad.Gamepads.First();

        Update();

        return Task.CompletedTask;
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