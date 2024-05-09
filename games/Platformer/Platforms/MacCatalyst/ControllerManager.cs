using System.Diagnostics;
using Foundation;
using GameController;
using UIKit;

namespace Platformer;

public partial class ControllerManager
{
    public ControllerManager()
    {
        GCController.Notifications.ObserveDidConnect(ConnectToController);
    }

    private void ConnectToController(object sender, NSNotificationEventArgs e)
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

    public async Task Initialise()
    {
        await GCController.StartWirelessControllerDiscoveryAsync();
    }

    private void Changed(GCPhysicalInputProfile gamepad, GCControllerElement element)
    {
        Debug.WriteLine($"{element}");

        if (element is GCControllerDirectionPad directionPad)
        {
            if (directionPad.XAxis.Value == 1)
            {
                CurrentPressedButton = ControllerButton.Right;
            }
            else if (directionPad.XAxis.Value == -1)
            {
                CurrentPressedButton = ControllerButton.Left;
            }
            else if (directionPad.YAxis.Value == 1)
            {
                CurrentPressedButton = ControllerButton.Up;
            }
            else if (directionPad.YAxis.Value == -1)
            {
                CurrentPressedButton = ControllerButton.Down;
            }
            else
            {
                CurrentPressedButton = ControllerButton.None;
            }

            DirectionalChange = new(directionPad.XAxis.Value, -directionPad.YAxis.Value);
        }
        else
        {
            CurrentPressedButton = ControllerButton.None;
        }
    }
}
