using BuildingGames.Slides;
using Orbit.Engine;

namespace BuildingGames;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly ControllerManager controllerManager;

    public MainPage(
        IGameSceneManager gameSceneManager,
        ControllerManager controllerManager)
	{
		InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.controllerManager = controllerManager;

        LoadSlide(SlideDeck.CurrentSlideType);

        this.controllerManager.Initialise();
        this.controllerManager.ButtonPressed += ControllerManager_ButtonPressed;
    }

    private void ControllerManager_ButtonPressed(ControllerButton controllerButton)
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

    async void LoadSlide(Type sceneType)
    {
        if (GameView.Scene is SlideSceneBase previousScene)
        {
            previousScene.Back -= OnCurrentSceneBack;
            previousScene.Next -= OnCurrentSceneNext;
        }

        if (sceneType.IsAssignableTo(typeof(SlideSceneBase)))
        {
            this.gameSceneManager.LoadScene(sceneType, GameView);

            if (GameView.Scene is SlideSceneBase nextScene)
            {
                nextScene.Back += OnCurrentSceneBack;
                nextScene.Next += OnCurrentSceneNext;
            }

            this.gameSceneManager.Start();
        }
        else if (sceneType.IsAssignableTo(typeof(ContentPage)))
        {
            try
            {
                await Shell.Current.GoToAsync($"/{sceneType.Name}");
            }
            catch (Exception ex)
            {

            }
            
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
        GoBack();
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        ProgressSlides();
    }

    void ProgressSlides()
    {
        if (GameView.Scene is SlideSceneBase slideSceneBase &&
            slideSceneBase.CanProgress)
        {
            slideSceneBase.Progress();
        }
    }

    void GoBack()
    {
        if (SlideDeck.GetPreviousSlideType() is Type previousSlideType)
        {
            this.LoadSlide(previousSlideType);
        }
    }
}
