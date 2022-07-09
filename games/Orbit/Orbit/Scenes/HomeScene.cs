using Orbit.Engine;
using Orbit.GameObjects;

namespace Orbit.Scenes;

public class HomeScene : GameScene
{
    public HomeScene(
        Ship ship,
        Sun sun,
        Planet planet)
    {
        Add(sun);
        Add(ship);
        Add(planet);
    }
}
