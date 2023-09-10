using System.Runtime.InteropServices;
using CoreAnimation;
using Foundation;
using UIKit;

namespace BuildingGames.Behaviors;

public partial class ParticleEmitterBehavior
{
    private UIView platformView;

    public ParticleEmitterBehavior()
    {
        EmitCommand = new Command(OnEmit);
    }

    protected override void OnAttachedTo(VisualElement bindable, UIView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        this.platformView = platformView;
    }

    protected override void OnDetachedFrom(VisualElement bindable, UIView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);
    }

    private void OnEmit()
    {
        var control = platformView;

        var lifeTime = LifeTime;
        var numberOfItems = NumberOfParticles;
        var scale = Scale;
        var speed = Speed * 1000;
        var image = Image;

        var emitterLayer = new CAEmitterLayer
        {
            Position = new CoreGraphics.CGPoint(
                control.Bounds.Location.X + control.Bounds.Width / 2,
                control.Bounds.Location.Y + control.Bounds.Height / 2),
            Shape = CAEmitterLayer.ShapeCircle
        };

        var cell = new CAEmitterCell
        {
            Name = "pEmitter",
            BirthRate = numberOfItems,
            Scale = 0f,
            ScaleRange = scale,
            Velocity = speed,
            LifeTime = (float)lifeTime,
            EmissionRange = (NFloat)Math.PI * 2.0f,
            Contents = UIImage.FromBundle(image).CGImage
        };

        emitterLayer.Cells = new CAEmitterCell[] { cell };

        control.Layer.AddSublayer(emitterLayer);

        Task.Run(async () =>
        {
            await Task.Delay(100).ConfigureAwait(false);

            Device.BeginInvokeOnMainThread(() =>
            {
                emitterLayer.SetValueForKeyPath(NSNumber.FromInt32(0), new NSString("emitterCells.pEmitter.birthRate"));
            });
        });
    }
}
