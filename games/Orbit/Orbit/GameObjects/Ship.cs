using Orbit.Engine;

namespace Orbit.GameObjects;

public class Ship : GameObject
{
    readonly Microsoft.Maui.Graphics.IImage slowDownImage;
    readonly Microsoft.Maui.Graphics.IImage speedUpImage;
    readonly Microsoft.Maui.Graphics.IImage image;
    private readonly IGameSceneManager gameSceneManager;
    private float batteryMaximum = 100f;
    private float batteryLevel = 100f;
    private float batteryDrain = 0.5f;
    public float angle = 0f;

    // TODO: Different types of collision here:
    // 1. Collide with Asteroid - damages ship
    // 2. Move into shadow - stop regenerating power

    public static float BatteryLevel { get; private set; }

    public override bool IsCollisionDetectionEnabled => true;

    public Ship(
        IGameSceneManager gameSceneManager,
        Gun gun)
    {
        image = LoadImage("ship_none.png");
        speedUpImage = LoadImage("ship_forward.png");
        slowDownImage = LoadImage("ship_reverse.png");
        this.gameSceneManager = gameSceneManager;

        Add(gun);
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
        canvas.DrawImage(GetImage(MainPage.TouchMode), orbitRadius, 0, Bounds.Width, Bounds.Height);

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

        angle += BatteryLevel == 0 ? -0.25f : GetIncrement(MainPage.TouchMode);

        batteryLevel = Math.Clamp(batteryLevel - GetBatteryDrain(MainPage.TouchMode), 0, batteryMaximum);
        BatteryLevel = batteryLevel / batteryMaximum;
    }

    private float GetBatteryDrain(TouchMode touchMode) => touchMode switch
    {
        TouchMode.SlowDown => batteryDrain,
        TouchMode.SpeedUp => batteryDrain,
        _ => -batteryDrain
    };

    private Microsoft.Maui.Graphics.IImage GetImage(TouchMode touchMode) => touchMode switch
    {
        TouchMode.SlowDown => slowDownImage,
        TouchMode.SpeedUp => speedUpImage,
        _ => image
    };

    private static float GetIncrement(TouchMode touchMode) => touchMode switch
    {
        TouchMode.None => -0.25f,
        TouchMode.SlowDown => -0.1f,
        TouchMode.SpeedUp => -1.2f,
        _ => 0f
    };
}
