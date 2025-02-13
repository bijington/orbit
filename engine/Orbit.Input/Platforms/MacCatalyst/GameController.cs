using System.Diagnostics;
using Foundation;
using GameController;

namespace Orbit.Input;

public partial class GameController
{
    public GameController()
    {
        GCController.Notifications.ObserveDidConnect(ConnectToController);
    }

    private void ConnectToController(object? sender, NSNotificationEventArgs e)
    {
        var controller = GCController.Controllers.FirstOrDefault();

        if (controller is null)
        {
            return;
        }

        if (controller.PhysicalInputProfile is not null)
        {
            controller.PhysicalInputProfile.ValueDidChangeHandler += Changed;
        }
    }

    public partial async Task Initialise()
    {
        await GCController.StartWirelessControllerDiscoveryAsync();
    }

    private void Changed(GCPhysicalInputProfile gamepad, GCControllerElement element)
    {
        Debug.WriteLine($"{element}");

        switch (element)
        {
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button A")):
                ButtonSouth = buttonInput.IsPressed;
                RaiseButtonPressed(nameof(ButtonSouth), ButtonSouth);
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button B")):
                ButtonEast = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button X")):
                ButtonWest = buttonInput.IsPressed;
                break;
            
            case GCControllerButtonInput buttonInput when buttonInput.Aliases.Contains(new NSString("Button Y")):
                ButtonNorth = buttonInput.IsPressed;
                RaiseButtonPressed(nameof(ButtonNorth), ButtonNorth);
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
                RaiseButtonValueChanged(nameof(ButtonNorth), LeftStick.XAxis);
                
                LeftStick.YAxis = directionPad.YAxis.Value;
                break;
            
            case GCControllerDirectionPad directionPad when directionPad.Aliases.Contains(new NSString("Right Thumbstick")):
                RightStick.XAxis = directionPad.XAxis.Value;
                RightStick.YAxis = directionPad.YAxis.Value;
                break;
        }
    }
}