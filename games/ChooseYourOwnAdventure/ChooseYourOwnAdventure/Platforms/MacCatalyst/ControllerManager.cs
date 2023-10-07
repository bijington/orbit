using System.Diagnostics;
using Foundation;
using GameController;
using UIKit;

namespace BuildingGames;

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
        UIScreen.Notifications.ObserveDidConnect(ScreenConnected);
        UIScreen.Notifications.ObserveModeDidChange(ScreenConnected);
        UIScreen.Notifications.ObserveReferenceDisplayModeStatusDidChange(ScreenConnected);

        var a = UIScreen.Screens.Count();
        var main = UIScreen.MainScreen;

        //UIScreen.MainScreen
        await GCController.StartWirelessControllerDiscoveryAsync();
    }

    public void ScreenConnected(object sender, NSNotificationEventArgs e)
    {
        var a = UIScreen.Screens.Count();
    }

    private void Changed(GCPhysicalInputProfile gamepad, GCControllerElement element)
    {
        Debug.WriteLine($"{element}");

        if (element is GCControllerButtonInput buttonInput)
        {
            if (buttonInput.IsPressed &&
                buttonInput.LocalizedName == "Menu Button" ||
                buttonInput.LocalizedName == "OPTIONS Button")
            {
                CurrentPressedButton = ControllerButton.Start;
            }
            else if (buttonInput.IsPressed &&
                buttonInput.Aliases.Contains(new NSString("Button A")))
            {
                CurrentPressedButton = ControllerButton.Accept;
            }
            else if (buttonInput.IsPressed &&
                buttonInput.Aliases.Contains(new NSString("Right Shoulder")))
            {
                CurrentPressedButton = ControllerButton.NavigateForward;
            }
            else if (buttonInput.IsPressed &&
                buttonInput.Aliases.Contains(new NSString("Left Shoulder")))
            {
                CurrentPressedButton = ControllerButton.NavigateBackward;
            }
            else
            {
                CurrentPressedButton = ControllerButton.None;
            }

            if (buttonInput.IsPressed &&
                buttonInput.Aliases.Contains(new NSString("Button Y")))
            {
                Mode = Mode == ControlMode.Pointer ? ControlMode.Navigation : ControlMode.Pointer;
            }
        }
        else if (element is GCControllerDirectionPad directionPad)
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
