namespace Orbit;

public class BatteryLevel : GameObject
{
    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        base.Render(canvas, dirtyRect);

        float height = 390f;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Gray;
        canvas.DrawLine(20, 10, 20, height);

        float actualLevelHeight = height * Ship.BatteryLevel;
        float y = height - actualLevelHeight + 10;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Red;
        canvas.DrawLine(40, y, 40, height);
    }
}
