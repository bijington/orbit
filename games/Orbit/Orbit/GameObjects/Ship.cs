using Orbit.Engine;

namespace Orbit.GameObjects;

public class Ship : GameObject
{
    readonly Microsoft.Maui.Graphics.IImage image;
    private readonly IGameSceneManager gameSceneManager;
    private readonly Thruster thruster;
    private readonly UserInputManager userInputManager;
    public float angle = 0f;

    // TODO: Different types of collision here:
    // 1. Collide with Asteroid - damages ship
    // 2. Move into shadow - stop regenerating power

    public override bool IsCollisionDetectionEnabled => true;

    public Ship(
        IGameSceneManager gameSceneManager,
        Thruster thruster,
        Gun gun,
        Battery battery,
        UserInputManager userInputManager)
    {
        image = LoadImage("ship.png");

        this.gameSceneManager = gameSceneManager;
        this.thruster = thruster;
        this.userInputManager = userInputManager;

        Add(gun);
        Add(thruster);
        Add(battery);

        gun.Ship = this;
        thruster.Ship = this;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        var orbitRadius = Math.Min(dimensions.Width, dimensions.Height) / 4;

        var theta = angle * MathF.PI / 180;
        var adjacent = MathF.Cos(theta) * orbitRadius;
        var opposite = MathF.Sin(theta) * orbitRadius;

        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;

        Bounds = new RectF(
            dimensions.Center.X + adjacent - halfWidth,
            dimensions.Center.Y + opposite - halfHeight,
            image.Width,
            image.Height);

        canvas.Translate(Bounds.X + halfWidth, Bounds.Y + halfHeight);
        canvas.Rotate(angle + 90);

        canvas.DrawImage(image, -halfWidth, -halfHeight, Bounds.Width, Bounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(-halfWidth, -halfHeight, Bounds.Width, Bounds.Height);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        angle = (angle + this.thruster.Thrust) % 360;
    }
}
