using Orbit.Engine;
using Orbit.GameObjects;

namespace Orbit;

public class MainScene : GameScene
{
    public MainScene(
        Ship ship,
        Sun sun,
        Planet planet,
        BatteryLevelIndicator batteryLevelIndicator,
        PlanetHealthIndicator planetHealthIndicator,
        AsteroidLauncher asteroidLauncher)
    {
        Add(sun);
        Add(ship);
        Add(asteroidLauncher);
        Add(planet);
        Add(batteryLevelIndicator);
        Add(planetHealthIndicator);
    }
}
