using Orbit.Engine;

namespace Orbit.GameObjects;

public class Asteroid : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    Microsoft.Maui.Graphics.IImage image;
    float x;
    float y;
    Movement movement;

    public Asteroid(IGameSceneManager gameSceneManager)
    {
        image = LoadImage("asteroid.png");

        this.gameSceneManager = gameSceneManager;
    }

    public void SetMovement(Movement movement)
    {
        this.movement = movement;
        x = movement.OriginX;
        y = movement.OriginY;
    }

    public override bool IsCollisionDetectionEnabled => true;

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        x += movement.SpeedX;
        y += movement.SpeedY;

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
            canvas.DrawLine(movement.OriginX, movement.OriginY, movement.DestinationX, movement.DestinationY);
        }

        var collision = gameSceneManager.FindCollision(this);

        if (collision is Planet planet)
        {
            planet.OnHit(25);
            CurrentScene.Remove(this);
        }

        if (collision is Ship ship)
        {
            CurrentScene.Remove(this);
        }

        // TODO: Allow collision with other asteroids.
        if (collision is Asteroid otherAsteroid)
        {
            // TODO: Split in to smaller asteroids?
            CurrentScene.Remove(otherAsteroid);
            CurrentScene.Remove(this);
        }

        // TODO: remove when off screen.
    }
}
