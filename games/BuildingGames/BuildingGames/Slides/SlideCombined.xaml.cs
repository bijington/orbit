using Orbit.Engine;

namespace BuildingGames.Slides;

public partial class SlideCombined : SlidePageBase
{
    private readonly IDispatcher dispatcher;
    private readonly CancellationTokenSource cancellationTokenSource;

    public SlideCombined(IDispatcher dispatcher, IGameSceneManager gameSceneManager, ControllerManager controllerManager) : base(gameSceneManager, controllerManager)
    {
        InitializeComponent();
        this.dispatcher = dispatcher;
        cancellationTokenSource = new CancellationTokenSource();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        PerformAnimation();
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);

        cancellationTokenSource.Cancel();
    }

    private void PerformAnimation()
    {
        dispatcher.DispatchDelayed(
            TimeSpan.FromSeconds(2),
            async () =>
            {
                try
                {
                    Tile.Scale = 1;
                    Tile.IsVisible = true;

                    await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationTokenSource.Token);

                    await Tile.RotateXTo(90, 100, Easing.BounceIn);
                    Tile.Content.IsVisible = !Tile.Content.IsVisible;
                    await Tile.RotateXTo(0, 100, Easing.BounceIn);

                    await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationTokenSource.Token);

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

                            ParticleEmitter.Emit();

                            if (cancellationTokenSource.IsCancellationRequested is false)
                            {
                                PerformAnimation();
                            }
                        });
                }
                catch (OperationCanceledException)
                {

                }
            });
    }
}
