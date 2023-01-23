using Orbit.Engine;

namespace Orbit.GameObjects;

public class BatteryLevelIndicator : GameObject
{
    private readonly Battery battery;

    public BatteryLevelIndicator(Battery battery)
    {
        this.battery = battery;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        float height = dimensions.Height / 2;
        float initialY = height / 2;

        canvas.Translate(0, dimensions.Center.Y);
        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Gray;
        canvas.DrawLine(30, -initialY, 30, initialY);

        float actualLevelHeight = height * this.battery.BatteryLevel;
        float y = height - actualLevelHeight;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.DarkBlue;
        canvas.DrawLine(30, y - initialY, 30, initialY);
    }
}
