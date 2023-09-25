using Orbit.Engine;

namespace Orbit.GameObjects;

public class Pulse : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly Ship ship;
    Microsoft.Maui.Graphics.IImage image;
    float x;
    float y;
    Movement movement;
    float originalAngle;
    readonly RadialGradientPaint pulsePaint;

    public Pulse(
        IGameSceneManager gameSceneManager,
        Ship ship)
    {
        image = LoadImage("pulse.png");

        this.gameSceneManager = gameSceneManager;
        this.ship = ship;

        pulsePaint = new RadialGradientPaint(
            new PaintGradientStop[]
            {
                new PaintGradientStop(0, Color.FromRgb(94, 87, 172)),
                new PaintGradientStop(0.5f, Color.FromRgb(137, 202, 240)),
                new PaintGradientStop(1f, Color.FromRgb(61, 54, 90))
            });
    }

    public void SetMovement(Movement movement, float angle)
    {
        this.movement = movement;
        x = movement.OriginX;
        y = movement.OriginY;
        originalAngle = angle;

        //x = 0.5f;
        //y = 0.5f;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Translate(dimensions.Center.X, dimensions.Center.Y);
        canvas.Rotate(ship.angle);
        var orbitRadius = Math.Min(dimensions.Width, dimensions.Height) / 4;
        canvas.Translate(orbitRadius, 0);
        canvas.Rotate(-36);

        // TODO: convert this to real world bounds.
        Bounds = new RectF(
            (x * dimensions.Width),
            (y * dimensions.Height),
            10,
            10);

        var transformedBounds = new RectF(
            x * dimensions.Width,
            y * dimensions.Height,
            10,
            10);
        canvas.SetFillPaint(pulsePaint, transformedBounds);

        canvas.FillEllipse(transformedBounds.X, transformedBounds.Y, transformedBounds.Width, transformedBounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(transformedBounds.X, transformedBounds.Y, transformedBounds.Width, transformedBounds.Height);

            canvas.StrokeColor = Colors.Orange;
            canvas.StrokeDashPattern = new[] { 1f, 2f };
            canvas.DrawLine(
                movement.OriginX * dimensions.Width,
                movement.OriginY * dimensions.Height,
                movement.DestinationX * dimensions.Width,
                movement.DestinationY * dimensions.Height);
        }
    }

    public override bool IsCollisionDetectionEnabled => true;

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
