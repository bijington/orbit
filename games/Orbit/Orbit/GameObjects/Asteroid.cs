using Orbit.Engine;

namespace Orbit.GameObjects;

public class Asteroid : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly IServiceProvider serviceProvider;
    Microsoft.Maui.Graphics.IImage image;
    float x;
    float y;
    Movement movement;

    public Asteroid(
        IGameSceneManager gameSceneManager,
        IServiceProvider serviceProvider)
    {
        image = LoadImage("asteroid.png");

        this.gameSceneManager = gameSceneManager;
        this.serviceProvider = serviceProvider;
    }

    public void SetMovement(Movement movement)
    {
        this.movement = movement;
        x = movement.OriginX;
        y = movement.OriginY;

        //x = 0.5f;
        //y = 0.5f;
    }

    public override bool IsCollisionDetectionEnabled => true;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        x += movement.SpeedX;
        y += movement.SpeedY;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var size = Math.Min(dimensions.Width, dimensions.Height) / 16;

        Bounds = new RectF(
            (x * dimensions.Width) - size,
            (y * dimensions.Height) - size,
            size * 2,
            size * 2);

        canvas.DrawImage(image, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

            canvas.StrokeColor = Colors.White;
            canvas.StrokeDashPattern = new[] { 1f, 2f };
            canvas.DrawLine(
                movement.OriginX * dimensions.Width,
                movement.OriginY * dimensions.Height,
                movement.DestinationX * dimensions.Width,
                movement.DestinationY * dimensions.Height);
        }

        var collision = gameSceneManager.FindCollision(this);

        if (collision is Planet planet)
        {
            planet.OnHit(25);
            CurrentScene.Remove(this);

            var remains = this.serviceProvider.GetRequiredService<AsteroidRemains>();
            remains.SetBounds(this.Bounds);
            CurrentScene.Add(remains);
        }

        if (collision is Ship ship)
        {
            CurrentScene.Remove(this);

            // TODO: Damage the ship;
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
