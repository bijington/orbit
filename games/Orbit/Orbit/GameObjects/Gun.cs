using Orbit.Engine;

namespace Orbit.GameObjects;

public class Gun : GameObject
{
    private readonly IServiceProvider serviceProvider;
    private double firingSpeed = 400;
    private double elapsed = 0;

    public Ship Ship { get; set; }

    public Gun(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        elapsed += millisecondsSinceLastUpdate;

        if (elapsed >= firingSpeed)
        {
            elapsed = 0;

            Pulse pulse = serviceProvider.GetRequiredService<Pulse>();

            pulse.SetMovement(
                new Movement(
                    new PointF(0, 0),
                    new Point(2, 2),
                    new Point(0.005, 0.005)),
                Ship.angle);

            CurrentScene.Add(pulse);
        }
    }
}
