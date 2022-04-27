namespace Orbit;

public class Scene : IDrawable
{
    private readonly BatteryLevel batteryLevel;
    private readonly Planet planet;
    private readonly Ship ship;

    public Scene()
    {
        batteryLevel = new BatteryLevel();
        planet = new Planet();
        ship = new Ship();
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        planet.Draw(canvas, dirtyRect);

        ship.Draw(canvas, dirtyRect);

        batteryLevel.Draw(canvas, dirtyRect);
    }
}
