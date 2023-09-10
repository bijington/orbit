using System.Diagnostics;
using BuildingGames.Platforms.MacCatalyst.ControllerChangedHandlers;
using Foundation;
using GameController;

namespace BuildingGames;

public partial class ControllerManager
{
    private readonly IDictionary<string, IControllerChangedHandler> supportedControllers;

    public ControllerManager()
    {
        supportedControllers = new Dictionary<string, IControllerChangedHandler>
        {
            ["DUALSHOCK 4 Wireless Controller"] = new DualShockControllerChangedHandler()
            //["usb gamepad           "] = 
        };

        GCController.Notifications.ObserveDidConnect(ConnectToController);

        GameKit.GKAchievement achievement = new GameKit.GKAchievement();
        achievement.PercentComplete = 100.0;
        achievement.ShowsCompletionBanner = true;
        GameKit.GKAchievement.ReportAchievements(
            new[] { achievement },
            errors =>
            {
                var a = 9;
            });
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
