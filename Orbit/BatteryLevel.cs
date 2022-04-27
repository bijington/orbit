namespace Orbit;

public class BatteryLevel : GameObject
{
    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        base.Render(canvas, dirtyRect);

        float height = 390f;
        float initialY = height / 2;

        canvas.Translate(0, dirtyRect.Center.Y);
        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Gray;
        canvas.DrawLine(30, -initialY, 30, initialY);

        float actualLevelHeight = height * Ship.BatteryLevel;
        float y = height - actualLevelHeight;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.DarkBlue;
        canvas.DrawLine(30, y - initialY, 30, initialY);
    }
}
