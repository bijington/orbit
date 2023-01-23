using Orbit.Engine;

namespace Orbit.GameObjects;

public class Battery : GameObject
{
    private const float batteryMaximum = 100f;
    private float batteryLevel = 100f;
    private const float batteryDrain = 0.5f;

    private bool isConsuming;
    private double cooldownMilliseconds;

    public float BatteryLevel { get; private set; }

    public bool ConsumeBatteryAmount(float amount)
    {
        if (amount == 0)
        {
            this.isConsuming = false;
            return false;
        }

        // TODO: Build in overheat/cooldown element.
        if (batteryLevel == 0)
        {
            cooldownMilliseconds = 500;
            this.isConsuming = false;
            return false;
        }

        batteryLevel = Math.Clamp(batteryLevel - amount, 0, batteryMaximum);
        BatteryLevel = batteryLevel / batteryMaximum;
        this.isConsuming = true;

        return true;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (!this.isConsuming)
        {
            if (cooldownMilliseconds == 0)
            {
                batteryLevel = Math.Clamp(batteryLevel + batteryDrain, 0, batteryMaximum);
                BatteryLevel = batteryLevel / batteryMaximum;
            }
            else
            {
                cooldownMilliseconds = Math.Clamp(cooldownMilliseconds - millisecondsSinceLastUpdate, 0, 500);
            }
        }

        this.isConsuming = false;
    }
}
