namespace BuildingGames.Slides;

public partial class SlideLottie : ContentPage
{
	public SlideLottie()
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
