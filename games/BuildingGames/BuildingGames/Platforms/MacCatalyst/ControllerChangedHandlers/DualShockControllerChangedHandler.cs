using Foundation;
using GameController;

namespace BuildingGames.Platforms.MacCatalyst.ControllerChangedHandlers;

public class DualShockControllerChangedHandler : IControllerChangedHandler
{
    public ControllerButton HandleChange(GCPhysicalInputProfile gamepad, GCControllerElement element)
    {
        if (element is GCControllerButtonInput buttonInput)
        {
            if (buttonInput.IsPressed &&
                buttonInput.LocalizedName == "OPTIONS Button")
            {
                return ControllerButton.Start;
            }
            else if (buttonInput.IsPressed &&
                buttonInput.Aliases.Contains(new NSString("Button A")))
            {
                return ControllerButton.Accept;
            }
            else
            {
                return ControllerButton.None;
            }
        }
        else if (element is GCControllerDirectionPad directionPad)
        {
            if (directionPad.XAxis.Value == 1)
            {
                return ControllerButton.Right;
            }
            else if (directionPad.XAxis.Value == -1)
            {
                return ControllerButton.Left;
            }
            else if (directionPad.YAxis.Value == 1)
            {
                return ControllerButton.Up;
            }
            else if (directionPad.YAxis.Value == -1)
            {
                return ControllerButton.Down;
            }
            else
            {
                return ControllerButton.None;
            }
        }
        else
        {
            return ControllerButton.None;
        }
    }
}

