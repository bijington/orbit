﻿using System.Diagnostics;
using Foundation;
using GameController;

namespace Orbit.Input;

public partial class GameController
{
    private readonly GCController controller;

    /// <summary>
    /// Creates a new instance of <see cref="GameController"/>.
    /// </summary>
    /// <param name="controller">The <see cref="GCController"/> that provides platform specific interaction.</param>
    public GameController(GCController controller)
    {
        this.controller = controller;
        
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

        Name = controller.VendorName ?? "Unknown";
        
        if (OperatingSystem.IsIOSVersionAtLeast(16))
        {
            controller.PhysicalInputProfile.ValueDidChangeHandler += Changed;
        }
    }

    private void Changed(GCPhysicalInputProfile gamepad, GCControllerElement element)
    {
        Debug.WriteLine($"{element}");

        switch (element)
        {
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button A")):
                South.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button B")):
                East.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button X")):
                West.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button Y")):
                North.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Right Shoulder")):
                RightShoulder.Button.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Right Trigger")):
                RightShoulder.Trigger.Value = buttonInput.Value;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Left Shoulder")):
                LeftShoulder.Button.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Left Trigger")):
                LeftShoulder.Trigger.Value = buttonInput.Value;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button Menu")):
                Pause.Value = buttonInput.IsPressed;
                break;
            
            case GCControllerDirectionPad directionPad when directionPad.Aliases.Contains(new NSString("Left Thumbstick")):
                LeftStick.XAxis.Value = directionPad.XAxis.Value;
                LeftStick.YAxis.Value = directionPad.YAxis.Value;
                break;
            
            case GCControllerDirectionPad directionPad when directionPad.Aliases.Contains(new NSString("Right Thumbstick")):
                RightStick.XAxis.Value = directionPad.XAxis.Value;
                RightStick.YAxis.Value = directionPad.YAxis.Value;
                break;
            
            case GCControllerButtonInput buttonInput:
                var buttonName = buttonInput.LocalizedName ?? "Unknown";
                
                RaiseUnmappedButtonChange(buttonName, buttonInput.IsPressed);
                break;
        }
    }
}