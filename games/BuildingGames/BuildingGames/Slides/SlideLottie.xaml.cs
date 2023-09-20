using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideLottie : SlidePageBase
{
    protected override int Transitions => 1;

    public SlideLottie(IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
    {
		InitializeComponent();

        Points.FontSize = Styling.ScaledFontSize(0.048);
        Points.Text = @"
- Built and open sourced by Airbnb

- Renders After Effects animations

- Impressively small JSON files

- Native rendering

- Amazing resources (free and paid) https://lottiefiles.com
";
    }

    protected override void Transition(int nextTransition)
    {
        base.Transition(nextTransition);

        if (nextTransition == 1)
        {
            Points.IsVisible = false;
            Sample.IsVisible = true;
            LottieAnimation.IsAnimationEnabled = true;
        }
    }
}
