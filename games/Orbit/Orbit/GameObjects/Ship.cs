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

        var a = MathF.Cos(angle) * orbitRadius;
        var o = MathF.Sin(angle) * orbitRadius;

        Console.WriteLine($"a = {a} o = {o}");

        Bounds = new RectF(
            dimensions.Center.X + a,
            dimensions.Center.Y + o,
            image.Width,
            image.Height);

        canvas.Translate(dimensions.Center.X, dimensions.Center.Y);
        canvas.Rotate(angle);
        canvas.Translate(orbitRadius, 0);
        canvas.Rotate(90);



        canvas.DrawImage(image, -Bounds.Width / 2, -Bounds.Height / 2, Bounds.Width, Bounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(-Bounds.Width / 2, -Bounds.Height / 2, Bounds.Width, Bounds.Height);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        angle += this.thruster.Thrust;
    }
}
