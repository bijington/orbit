using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideAnimations : SlidePageBase
{
    private readonly IDispatcher dispatcher;

    public SlideAnimations(IDispatcher dispatcher, IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
    {
        InitializeComponent();
        this.dispatcher = dispatcher;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        CodeSample.Text = @"
// Rotate 90 degress
await Tile.RotateXTo(90, 100, Easing.BounceIn);

// Make the shape appear/disappear
Tile.Content.IsVisible = !Tile.Content.IsVisible;

// Rotate the remaining 90 degrees
await Tile.RotateXTo(0, 100, Easing.BounceIn);";

        PerformAnimation();
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
}
