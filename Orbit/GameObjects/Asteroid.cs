using Orbit.Engine;

namespace Orbit.GameObjects;

public class Asteroid : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    Microsoft.Maui.Graphics.IImage image;
    PointF destination;
    PointF origin;
    PointF speed;
    float x;
    float y;

    public Asteroid(IGameSceneManager gameSceneManager)
    {
        image = LoadImage("asteroid.png");


        // Randomly generate locations + speed.
        //origin = new PointF(0, 0);
        //destination = new PointF(200, 200);
        this.gameSceneManager = gameSceneManager;
    }

    public void SetTrajectory(PointF origin, PointF destination, PointF speed)
    {
        this.origin = origin;
        x = origin.X;
        y = origin.Y;
        this.destination = destination;
        this.speed = speed;
    }

    public override bool IsCollisionDetectionEnabled => true;

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        x += speed.X;
        y += speed.Y;

        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;

        Bounds = new RectF(x + -halfWidth, y + -halfHeight, image.Width, image.Height);

        canvas.DrawImage(image, x + -halfWidth, y + -halfHeight, image.Width, image.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(x + -halfWidth, y + -halfHeight, image.Width, image.Height);

            canvas.StrokeColor = Colors.White;
            canvas.StrokeDashPattern = new[] { 1f, 2f };
            canvas.DrawLine(origin, destination);
        }

        var collision = gameSceneManager.FindCollision(this);

        if (collision is Planet ||
            collision is Ship)
        {
            CurrentScene.Remove(this);
        }

        // TODO: remove when off screen.
    }
}
