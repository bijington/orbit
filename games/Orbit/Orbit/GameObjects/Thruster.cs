using Orbit.Engine;

namespace Orbit.GameObjects;

public class Thruster : GameObject
{
    private readonly Battery battery;
    private readonly UserInputManager userInputManager;
    private const float batteryDrain = 0.5f;

    public Thruster(
        Battery battery,
        UserInputManager userInputManager)
	{
        this.battery = battery;
        this.userInputManager = userInputManager;
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
