using Orbit.Engine;
using Orbit.GameObjects;

namespace Orbit;

public class MainScene : GameScene
{
    public MainScene(
        Ship ship,
        Sun sun,
        Planet planet,
        Orbit.GameObjects.Shadow shadow,
        BatteryLevel batteryLevel,
        AsteroidLauncher asteroidLauncher)
    {
        Add(sun);
        Add(ship);
        // TODO: shadow could be child of planet?
        Add(shadow);
        Add(planet);
        Add(batteryLevel);
        Add(asteroidLauncher);
    }
}
