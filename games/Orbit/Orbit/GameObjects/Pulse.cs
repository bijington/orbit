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

    public Pulse(
        IGameSceneManager gameSceneManager,
        Ship ship)
    {
        image = LoadImage("pulse.png");

        this.gameSceneManager = gameSceneManager;
        this.ship = ship;
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

        Bounds = new RectF(
            (x * dimensions.Width),
            (y * dimensions.Height),
            image.Width,
            image.Height);

        canvas.Translate(dimensions.Center.X, dimensions.Center.Y);
        canvas.Rotate(originalAngle);
        canvas.Translate(ship.Bounds.Width / 4, 0);
        canvas.Rotate(-30);

        canvas.DrawImage(image, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

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
