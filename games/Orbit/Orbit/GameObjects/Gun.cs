using Orbit.Audio;
using Orbit.Engine;

namespace Orbit.GameObjects;

public class Gun : GameObject
{
    private readonly IServiceProvider serviceProvider;
    private readonly AudioService audioService;
    private double firingSpeed = 400;
    private double elapsed = 0;

    public Ship Ship { get; set; }

    public Gun(IServiceProvider serviceProvider, AudioService audioService)
    {
        this.serviceProvider = serviceProvider;
        this.audioService = audioService;
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

            this.audioService.Stop(AudioItem.SoundEffect.Pulse);
            _ = this.audioService.Play(AudioItem.SoundEffect.Pulse, false);
        }
    }
}
