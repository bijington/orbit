using Orbit.Engine;

namespace Orbit.GameObjects;

public class Pulse : GameObject
{
    float x;
    float y;
    Movement movement;
    float originalAngle;
    readonly RadialGradientPaint pulsePaint;
    private readonly SettingsManager settingsManager;

    public Pulse(SettingsManager settingsManager)
    {
        pulsePaint = new RadialGradientPaint(
            new PaintGradientStop[]
            {
                new PaintGradientStop(0, Color.FromRgb(94, 87, 172)),
                new PaintGradientStop(0.5f, Color.FromRgb(137, 202, 240)),
                new PaintGradientStop(1f, Color.FromRgb(61, 54, 90))
            });
        this.settingsManager = settingsManager;
    }

    public void SetMovement(Movement movement, float angle)
    {
        this.movement = movement;
        x = movement.OriginX;
        y = movement.OriginY;

        originalAngle = angle;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var orbitRadius = Math.Min(dimensions.Width, dimensions.Height) / 4;

        var theta = originalAngle * MathF.PI / 180;
        var adjacent = MathF.Cos(theta) * (x * dimensions.Width);
        var opposite = MathF.Sin(theta) * (y * dimensions.Height);

        var shipAdjacent = MathF.Cos(theta) * orbitRadius;
        var shipOpposite = MathF.Sin(theta) * orbitRadius;

        var halfWidth = 5;
        var halfHeight = 5;

        Bounds = new RectF(
            dimensions.Center.X + adjacent + shipAdjacent - halfWidth,
            dimensions.Center.Y + opposite + shipOpposite - halfHeight,
            10,
            10);

        canvas.SetFillPaint(pulsePaint, Bounds);

        canvas.FillEllipse(Bounds);

        if (settingsManager.ShowDebug)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(Bounds);

            canvas.StrokeColor = Colors.Orange;
            canvas.StrokeDashPattern = new[] { 1f, 2f };
            canvas.DrawLine(
                movement.OriginX * dimensions.Width,
                movement.OriginY * dimensions.Height,
                movement.DestinationX * dimensions.Width,
                movement.DestinationY * dimensions.Height);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        x += movement.SpeedX;
        y += movement.SpeedY;

        if (x <= -0.1 || x >= 1.1 || y <= -0.1 || y >= 1.1)
        {
            CurrentScene.Remove(this);
        }
    }
}
