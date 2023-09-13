using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideAnimations : SlidePageBase
{
    private readonly IDispatcher dispatcher;

    protected override int Transitions => 1;

    public SlideAnimations(IDispatcher dispatcher, IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
    {
        InitializeComponent();
        this.dispatcher = dispatcher;

        Points.FontSize = Styling.ScaledFontSize(0.048);
        Points.Text = @"
- Amazingly powerful and yet simple API

- Pre built animations provided with Xamarin.Forms

- Ability to manipulate built in controls";
    }

    private void PerformAnimation()
    {
        dispatcher.DispatchDelayed(
            TimeSpan.FromSeconds(1),
            async () =>
            {
                await Tile.RotateXTo(90, 100, Easing.BounceIn);
                Tile.Content.IsVisible = !Tile.Content.IsVisible;
                await Tile.RotateXTo(0, 100, Easing.BounceIn);

                PerformAnimation();
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

            PerformAnimation();
        }
    }
}
