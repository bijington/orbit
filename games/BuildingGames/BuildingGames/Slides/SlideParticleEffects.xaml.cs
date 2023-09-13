using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideParticleEffects : SlidePageBase
{
    private readonly IDispatcher dispatcher;

    public SlideParticleEffects(IDispatcher dispatcher, IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
	{
		InitializeComponent();

        CodeSample.Text = @"
var emitterLayer = new CAEmitterLayer
{
    Shape = CAEmitterLayer.ShapeCircle
};

var cell = new CAEmitterCell
{
    Name = ""pEmitter"",
    BirthRate = numberOfItems,
    Scale = 0f,
    ScaleRange = scale,
    Velocity = speed,
    LifeTime = (float)lifeTime,
    EmissionRange = (NFloat)Math.PI * 2.0f,
    Contents = UIImage.FromBundle(image).CGImage
};

emitterLayer.Cells = new CAEmitterCell[] { cell };

control.Layer.AddSublayer(emitterLayer);";
        this.dispatcher = dispatcher;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        EmitParticles();
    }

    private void EmitParticles()
    {
        dispatcher.DispatchDelayed(
            TimeSpan.FromSeconds(1),
            () =>
            {
                ParticleEmitter.Emit();

                EmitParticles();
            });
    }
}
