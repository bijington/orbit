using Orbit.Engine;

namespace BuildingGames.Slides;

public abstract class SlidePageBase : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly ControllerManager controllerManager;

    private int CurrentTransition { get; set; }

    protected virtual int Transitions { get; }

    public SlidePageBase(IGameSceneManager gameSceneManager, ControllerManager controllerManager)
	{
        this.gameSceneManager = gameSceneManager;
        this.controllerManager = controllerManager;

        SlideDeck.Notes = this.Notes;
    }

    protected virtual string Notes { get; }

    protected virtual void Transition(int nextTransition)
    {
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
        else if (this.controllerManager.Mode == ControlMode.Navigation &&
            controllerButton == ControllerButton.NavigateBackward)
        {
            GoBack();
        }
    }

    private void Tap_Tapped(object sender, TappedEventArgs e)
    {
        ProgressSlides();
    }

    async void LoadSlide(Type sceneType)
    {
        if (sceneType.IsAssignableTo(typeof(SlideSceneBase)))
        {
            try
            {
                await Shell.Current.GoToAsync($"/{nameof(MainPage)}");
            }
            catch (Exception)
            {

            }
        }
        else if (sceneType.IsAssignableTo(typeof(ContentPage)))
        {
            try
            {
                await Shell.Current.GoToAsync($"/{sceneType.Name}");
            }
            catch (Exception)
            {

            }
        }
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        ProgressSlides();
    }

    protected virtual void ProgressSlides()
    {
        // If we are complete then fire the Next event.
        if (CurrentTransition == Transitions)
        {
            if (SlideDeck.GetNextSlideType() is Type nextSlideType)
            {
                this.LoadSlide(nextSlideType);
            }
        }

        CurrentTransition++;

        Transition(CurrentTransition);
    }

    protected void GoBack()
    {
        if (SlideDeck.GetPreviousSlideType() is Type previousSlideType)
        {
            this.LoadSlide(previousSlideType);
        }
    }
}
