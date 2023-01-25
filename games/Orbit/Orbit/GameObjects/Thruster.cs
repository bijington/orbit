using AVFoundation;
using Orbit.Audio;
using Orbit.Engine;
using Plugin.Maui.Audio;

namespace Orbit.GameObjects;

public class Thruster : GameObject
{
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
    }

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
            _ = this.audioService.Play(AudioItem.ThrusterSoundEffect, false);
        }
        else
        {
            this.audioService.Stop(AudioItem.ThrusterSoundEffect);
        }

        base.Update(millisecondsSinceLastUpdate);
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
}
