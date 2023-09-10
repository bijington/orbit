using Orbit.Engine;

namespace BuildingGames.Slides;

public abstract class SlidePageBase : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly ControllerManager controllerManager;

    public SlidePageBase(IGameSceneManager gameSceneManager, ControllerManager controllerManager)
	{
        this.gameSceneManager = gameSceneManager;
        this.controllerManager = controllerManager;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        var tap = new TapGestureRecognizer
        {

        };

        tap.Tapped += Tap_Tapped;

        this.Content.GestureRecognizers.Add(tap);

        if (this.Content is Grid grid)
        {
            var gameSceneView = new GameSceneView();
            grid.Add(gameSceneView);

            Grid.SetColumnSpan(gameSceneView, grid.ColumnDefinitions.Count);
            Grid.SetRowSpan(gameSceneView, grid.RowDefinitions.Count);

            this.gameSceneManager.LoadScene<PointerOverlay>(gameSceneView);
            this.gameSceneManager.Start();
        }

        this.controllerManager.ButtonPressed += OnControllerManagerButtonPressed;
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        this.controllerManager.ButtonPressed -= OnControllerManagerButtonPressed;
    }

    private void OnControllerManagerButtonPressed(ControllerButton controllerButton)
    {
        if (this.controllerManager.Mode == ControlMode.Navigation &&
            controllerButton == ControllerButton.NavigateForward)
        {
            ProgressSlides();
        }
    }

    private void Tap_Tapped(object sender, TappedEventArgs e)
    {
        OnCurrentSceneNext(null);
    }

    async void LoadSlide(Type sceneType)
    {
        if (sceneType.IsAssignableTo(typeof(SlideSceneBase)))
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
        else if (sceneType.IsAssignableTo(typeof(ContentPage)))
        {
            await Shell.Current.GoToAsync(sceneType.Name);
        }
    }

    private void OnCurrentSceneNext(SlideSceneBase sender)
    {
        if (SlideDeck.GetNextSlideType() is Type nextSlideType)
        {
            this.LoadSlide(nextSlideType);
        }
    }

    private void OnCurrentSceneBack(SlideSceneBase sender)
    {
        if (SlideDeck.GetPreviousSlideType() is Type previousSlideType)
        {
            this.LoadSlide(previousSlideType);
        }
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        ProgressSlides();
    }

    protected virtual void ProgressSlides()
    {
        OnCurrentSceneNext(null);
    }
}
