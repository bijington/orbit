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
    float angle = 0f;

    // TODO: Different types of collision here:
    // 1. Collide with Asteroid - damages ship
    // 2. Move into shadow - stop regenerating power

    public static float BatteryLevel { get; private set; }

    public Ship(IGameSceneManager gameSceneManager)
    {
        image = LoadImage("ship_none.png");
        speedUpImage = LoadImage("ship_forward.png");
        slowDownImage = LoadImage("ship_reverse.png");
        this.gameSceneManager = gameSceneManager;
    }

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Translate(dirtyRect.Center.X, dirtyRect.Center.Y);
        canvas.Rotate(angle);
        canvas.DrawImage(GetImage(MainPage.TouchMode), 300, 0, image.Width, image.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(300, 0, image.Width, image.Height);
        }

        batteryLevel = Math.Clamp(batteryLevel - GetBatteryDrain(MainPage.TouchMode), 0, batteryMaximum);
        BatteryLevel = batteryLevel / batteryMaximum;

        angle += BatteryLevel == 0 ? -0.25f : GetIncrement(MainPage.TouchMode);
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
