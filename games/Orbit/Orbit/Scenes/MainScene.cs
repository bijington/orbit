using Orbit.Engine;
using Orbit.GameObjects;

namespace Orbit;

public class MainScene : GameScene
{
    public MainScene(
        Ship ship,
        Sun sun,
        Planet planet,
        BatteryLevelIndicator BatteryLevelIndicator,
        AsteroidLauncher asteroidLauncher)
    {
        Add(sun);
        Add(ship);
        Add(asteroidLauncher);
        Add(planet);
        Add(BatteryLevelIndicator);
    }
}
