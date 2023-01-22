using Orbit.Engine;

namespace Orbit.GameObjects;

public class Thruster : GameObject
{
    private readonly Battery battery;

    private const float batteryDrain = 0.5f;

    public Thruster(Battery battery)
	{
        this.battery = battery;
    }

    public float Thrust { get; private set; }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (this.battery.ConsumeBatteryAmount(GetBatteryDrain(MainPage.TouchMode)))
        {
            Thrust = GetThrust(MainPage.TouchMode);
        }
        else
        {
            Thrust = -0.25f;
        }
    }

    private float GetBatteryDrain(TouchMode touchMode) => touchMode switch
    {
        TouchMode.SlowDown => batteryDrain,
        TouchMode.SpeedUp => batteryDrain,
        _ => -batteryDrain
    };

    public float GetThrust(TouchMode touchMode) => touchMode switch
    {
        TouchMode.None => -0.25f,
        TouchMode.SlowDown => -0.1f,
        TouchMode.SpeedUp => -1.2f,
        _ => 0f
    };
}
