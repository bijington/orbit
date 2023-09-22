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

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.StrokeColor = Colors.Green;
        canvas.StrokeSize = 5;
        canvas.DrawRectangle(ship.Bounds);

        foreach (var pulse in this.GameObjectsSnapshot.OfType<Pulse>())
        {
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeSize = 5;
            canvas.DrawRectangle(pulse.Bounds);
        }
    }
}
