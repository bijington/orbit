using Orbit.Engine;

namespace Orbit.GameObjects;

public class Battery : GameObject
{
    private const float batteryMaximum = 100f;
    private float batteryLevel = 100f;
    private const float batteryDrain = 0.5f;

    public float BatteryLevel { get; private set; }

    public bool ConsumeBatteryAmount(float amount)
    {
        // TODO: Build in overheat/cooldown element.
        if (batteryLevel == 0)
        {
            return false;
        }

        batteryLevel = Math.Clamp(batteryLevel - amount, 0, batteryMaximum);
        BatteryLevel = batteryLevel / batteryMaximum;

        return true;
    }
}
