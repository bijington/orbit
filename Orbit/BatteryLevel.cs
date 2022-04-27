namespace Orbit;

public class BatteryLevel : GameObject
{
    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        base.Render(canvas, dirtyRect);

        float height = 390f;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Gray;
        canvas.DrawLine(30, 10, 30, height);

        float actualLevelHeight = height * Ship.BatteryLevel;
        float y = height - actualLevelHeight + 10;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Red;
        canvas.DrawLine(30, y, 30, height);
    }
}
