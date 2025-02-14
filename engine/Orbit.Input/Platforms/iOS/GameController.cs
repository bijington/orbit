using System.Diagnostics;
using Foundation;
using GameController;

namespace Orbit.Input;

public partial class GameController
{
    private readonly GCController controller;

    public GameController(GCController controller)
    {
        this.controller = controller;
        
        Dpad = new Stick(this, nameof(Dpad));
        LeftStick = new Stick(this, nameof(LeftStick));
        RightStick = new Stick(this, nameof(RightStick));

        LeftShoulder = new Shoulder(this, nameof(LeftShoulder));
        RightShoulder = new Shoulder(this, nameof(RightShoulder));
        
        controller.PhysicalInputProfile.ValueDidChangeHandler += Changed;
    }

    private void Changed(GCPhysicalInputProfile gamepad, GCControllerElement element)
    {
        Debug.WriteLine($"{element}");

        switch (element)
        {
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button A")):
                ButtonSouth = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button B")):
                ButtonEast = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button X")):
                ButtonWest = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button Y")):
                ButtonNorth = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Right Shoulder")):
                RightShoulder.Button = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Right Trigger")):
                RightShoulder.Trigger = buttonInput.Value;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Left Shoulder")):
                LeftShoulder.Button = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Left Trigger")):
                LeftShoulder.Trigger = buttonInput.Value;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button Menu")):
                Pause = buttonInput.IsPressed;
                break;
            
            case GCControllerDirectionPad directionPad when directionPad.Aliases.Contains(new NSString("Left Thumbstick")):
                LeftStick.XAxis = directionPad.XAxis.Value;
                LeftStick.YAxis = directionPad.YAxis.Value;
                break;
            
            case GCControllerDirectionPad directionPad when directionPad.Aliases.Contains(new NSString("Right Thumbstick")):
                RightStick.XAxis = directionPad.XAxis.Value;
                RightStick.YAxis = directionPad.YAxis.Value;
                break;
        }
    }
}