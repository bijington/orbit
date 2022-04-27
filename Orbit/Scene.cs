using System.Diagnostics;

namespace Orbit;

public class Scene : IDrawable
{
    private readonly BatteryLevel batteryLevel;
    private readonly Planet planet;
    private readonly Ship ship;
    private readonly MainPage mainPage;

    private readonly Spawner spawner;
    private readonly GameObject asteroid;

    public Scene(MainPage mainPage)
    {
        batteryLevel = new BatteryLevel();
        planet = new Planet();
        ship = new Ship();
        spawner = new Spawner();
        asteroid = spawner.Spawn();
        this.mainPage = mainPage;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        planet.Draw(canvas, dirtyRect);

        ship.Draw(canvas, dirtyRect);

        batteryLevel.Draw(canvas, dirtyRect);

        asteroid.Draw(canvas, dirtyRect);

        stopwatch.Stop();

        var rate = 1 / (0.016 + stopwatch.ElapsedMilliseconds);

        mainPage.SetText($"FPS: {rate}");
    }
}
