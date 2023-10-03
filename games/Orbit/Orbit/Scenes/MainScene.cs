using Orbit.Engine;
using Orbit.GameObjects;

namespace Orbit;

public class MainScene : GameScene
{
    private readonly Ship ship;

    public MainScene(
        Ship ship,
        Sun sun,
        Planet planet,
        BatteryLevelIndicator batteryLevelIndicator,
        PlanetHealthIndicator planetHealthIndicator,
        AsteroidLauncher asteroidLauncher,
        VersionOverlay versionOverlay)
    {
        Add(sun);
        Add(ship);
        Add(asteroidLauncher);
        Add(planet);
        Add(batteryLevelIndicator);
        Add(planetHealthIndicator);
        Add(versionOverlay);
        this.ship = ship;
    }
}
