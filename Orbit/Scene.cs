using Orbit.Engine;

namespace Orbit;

public class Scene : IGameScene
{
    private readonly BatteryLevel batteryLevel;
    private readonly Planet planet;
    private readonly Ship ship;
    private readonly Sun sun;
    readonly Shadow shadow;

    private readonly Spawner spawner;
    private readonly GameObject asteroid;

    public Scene()
    {
        batteryLevel = new BatteryLevel();
        planet = new Planet();
        ship = new Ship();
        sun = new Sun();
        spawner = new Spawner();
        asteroid = spawner.Spawn();
        shadow = new Shadow(planet);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        sun.Draw(canvas, dirtyRect);

        ship.Draw(canvas, dirtyRect);

        shadow.Draw(canvas, dirtyRect);

        planet.Draw(canvas, dirtyRect);

        batteryLevel.Draw(canvas, dirtyRect);

        asteroid.Draw(canvas, dirtyRect);
    }
}
