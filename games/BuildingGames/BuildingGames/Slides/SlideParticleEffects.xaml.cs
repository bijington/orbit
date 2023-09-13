using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideParticleEffects : SlidePageBase
{
    private readonly IDispatcher dispatcher;

    protected override int Transitions => 1;

    public SlideParticleEffects(IDispatcher dispatcher, IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
	{
		InitializeComponent();

        this.dispatcher = dispatcher;

        Points.Text = @"
- Makes use of .NET MAUI platform behaviors

- Allows customisation of existing native controls

- Community blog post - Rendy Del Rosario

- https://www.xamboy.com/2019/01/30/particle-system-in-xamarin-forms/

- If it is possible on the platform then it is possible in .NET MAUI";
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

    protected override void Transition(int nextTransition)
    {
        base.Transition(nextTransition);

        if (nextTransition == 1)
        {
            Points.IsVisible = false;
            Sample.IsVisible = true;
            Tile.IsVisible = true;

            EmitParticles();
        }
    }
}
