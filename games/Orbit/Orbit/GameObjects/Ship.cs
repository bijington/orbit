using Orbit.Engine;

namespace Orbit.GameObjects;

public class Ship : GameObject
{
    readonly Microsoft.Maui.Graphics.IImage slowDownImage;
    readonly Microsoft.Maui.Graphics.IImage speedUpImage;
    readonly Microsoft.Maui.Graphics.IImage image;
    private readonly IGameSceneManager gameSceneManager;
    private readonly Thruster thruster;
    public float angle = 0f;

    // TODO: Different types of collision here:
    // 1. Collide with Asteroid - damages ship
    // 2. Move into shadow - stop regenerating power

    public override bool IsCollisionDetectionEnabled => true;

    public Ship(
        IGameSceneManager gameSceneManager,
        Thruster thruster,
        Gun gun,
        Battery battery)
    {
        image = LoadImage("ship_none.png");
        speedUpImage = LoadImage("ship_forward.png");
        slowDownImage = LoadImage("ship_reverse.png");

        this.gameSceneManager = gameSceneManager;
        this.thruster = thruster;

        Add(gun);
        Add(thruster);
        Add(battery);

        gun.Ship = this;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var size = Math.Min(dimensions.Width, dimensions.Height) / 20;

        Bounds = new RectF(
            dimensions.Center.X - size,
            dimensions.Center.Y - size,
            size * 2,
            size * 2);

        var orbitRadius = Bounds.Width * 3;

        canvas.Translate(dimensions.Center.X, dimensions.Center.Y);
        canvas.Rotate(angle);
        var image = this.thruster.IsThrusting ? GetImage(MainPage.TouchMode) : GetImage(TouchMode.None);
        canvas.DrawImage(image, orbitRadius, 0, Bounds.Width, Bounds.Height);

        //Bounds = WHAT???? Needs to include rotation and translation details ;(

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(orbitRadius, 0, Bounds.Width, Bounds.Height);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        angle += this.thruster.Thrust;
    }

    private Microsoft.Maui.Graphics.IImage GetImage(TouchMode touchMode) => touchMode switch
    {
        TouchMode.SlowDown => slowDownImage,
        TouchMode.SpeedUp => speedUpImage,
        _ => image
    };
}
