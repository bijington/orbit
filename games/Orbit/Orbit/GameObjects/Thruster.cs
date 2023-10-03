using Orbit.Audio;
using Orbit.Engine;

namespace Orbit.GameObjects;

public class Thruster : GameObject
{
    readonly Microsoft.Maui.Graphics.IImage slowDownImage;
    readonly Microsoft.Maui.Graphics.IImage speedUpImage;
    private readonly Battery battery;
    private readonly UserInputManager userInputManager;
    private readonly AudioService audioService;
    private const float batteryDrain = 0.5f;

    public Thruster(
        Battery battery,
        UserInputManager userInputManager,
        AudioService audioService)
	{
        this.battery = battery;
        this.userInputManager = userInputManager;
        this.audioService = audioService;

        speedUpImage = LoadImage("thruster_fast.png");
        slowDownImage = LoadImage("thruster_slow.png");
    }

    public Ship Ship { get; set; }

    public float Thrust { get; private set; }

    public bool IsThrusting => Thrust != -0.25f;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        if (this.battery.ConsumeBatteryAmount(Thruster.GetBatteryDrain(userInputManager.TouchMode)))
        {
            Thrust = Thruster.GetThrust(userInputManager.TouchMode);
        }
        else
        {
            Thrust = -0.25f;
        }

        if (this.IsThrusting)
        {
            _ = this.audioService.Play(AudioItem.SoundEffect.Thruster, false);
        }
        else
        {
            this.audioService.Stop(AudioItem.SoundEffect.Thruster);
        }

        base.Update(millisecondsSinceLastUpdate);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var image = this.IsThrusting ? GetImage(userInputManager.TouchMode) : GetImage(TouchMode.None);

        if (image is not null)
        {
            canvas.DrawImage(image, -Ship.Bounds.Width / 2, -Ship.Bounds.Height / 2, Ship.Bounds.Width, Ship.Bounds.Height);
        }
    }

    private static float GetBatteryDrain(TouchMode touchMode) => touchMode switch
    {
        TouchMode.SlowDown => batteryDrain,
        TouchMode.SpeedUp => batteryDrain,
        _ => 0
    };

    public static float GetThrust(TouchMode touchMode) => touchMode switch
    {
        TouchMode.None => -0.25f,
        TouchMode.SlowDown => -0.1f,
        TouchMode.SpeedUp => -1.2f,
        _ => 0f
    };

    private Microsoft.Maui.Graphics.IImage GetImage(TouchMode touchMode) => touchMode switch
    {
        TouchMode.SlowDown => slowDownImage,
        TouchMode.SpeedUp => speedUpImage,
        _ => null
    };
}
