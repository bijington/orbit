using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideAnimationsPartTwo : SlidePageBase
{
    private readonly IDispatcher dispatcher;

    public SlideAnimationsPartTwo(IDispatcher dispatcher, IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
    {
        InitializeComponent();
        this.dispatcher = dispatcher;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        PerformAnimation();
    }

    private void PerformAnimation()
    {
        dispatcher.DispatchDelayed(
            TimeSpan.FromSeconds(1),
            async () =>
            {
                Tile.Scale = 1;
                Tile.IsVisible = true;

                await Task.Delay(TimeSpan.FromMilliseconds(700));

                var animation = new Animation
                {
                    { 0.0, 0.2, new Animation(v => Tile.Scale = v, 1, 0.9) },
                    { 0.2, 0.75, new Animation(v => Tile.Scale = v, 0.9, 1.2) },
                    { 0.75, 1.0, new Animation(v => Tile.Scale = v, 1.2, 0) }
                };

                animation.Commit(
                    Tile,
                    "SuccessfulMatch",
                    length: 500,
                    easing: Easing.SpringIn,
                    finished: (v, f) =>
                    {
                        Tile.IsVisible = false;
                    });

                PerformAnimation();
            });
    }
}
