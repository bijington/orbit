using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideLottie : SlidePageBase
{
	public SlideLottie(IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
    {
		InitializeComponent();

		CodeSample.Text =@"
    <controls:SKLottieView
        Source = ""trophy.json""
        RepeatCount = ""100""
        RepeatMode = ""Restart""
        IsAnimationEnabled = ""true""
        VerticalOptions = ""Center""
        HorizontalOptions = ""Center""
        WidthRequest = ""400""
        HeightRequest = ""400"" />";
    }
}
