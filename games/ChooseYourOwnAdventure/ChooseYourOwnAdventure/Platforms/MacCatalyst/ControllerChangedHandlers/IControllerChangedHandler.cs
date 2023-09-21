using GameController;

namespace BuildingGames.Platforms.MacCatalyst.ControllerChangedHandlers;

public interface IControllerChangedHandler
{
    ControllerButton HandleChange(GCPhysicalInputProfile gamepad, GCControllerElement element);
}

