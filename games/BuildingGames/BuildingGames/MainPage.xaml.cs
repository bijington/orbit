using BuildingGames.Scenes;
using Orbit.Engine;

namespace BuildingGames;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly ControllerManager controllerManager;

    public IList<Type> slides = new List<Type>()
    {
        typeof(Slide01),
        typeof(Slide02),
        typeof(Slide03),
        typeof(Slide04)
    };

    public MainPage(
        IGameSceneManager gameSceneManager,
        ControllerManager controllerManager)
	{
		InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.controllerManager = controllerManager;

        LoadSlide(this.slides.First());
        //gameSceneManager.Start();

#if MACCATALYST
        //this.controllerManager.Initialise();
        //this.controllerManager.ButtonPressed += ControllerManager_ButtonPressed;
#endif
    }

    private void ControllerManager_ButtonPressed(ControllerButton controllerButton)
    {
        //if (controllerButton == ControllerButton.NavigateForward &&
        //    GameView.Scene is SlideSceneBase slideSceneBase &&
        //    slideSceneBase.CanProgress)
        //{
        //    var nextSceneIndex = this.slides.IndexOf(GameView.Scene.GetType()) + 1;

        //    this.gameSceneManager.LoadScene(this.slides[Math.Clamp(nextSceneIndex, 0, this.slides.Count - 1)], GameView);
        //    this.gameSceneManager.Start();
        //}
        //else if (controllerButton == ControllerButton.NavigateBackward)
        //{
        //    var nextSceneIndex = this.slides.IndexOf(GameView.Scene.GetType()) - 1;

        //    this.gameSceneManager.LoadScene(this.slides[Math.Clamp(nextSceneIndex, 0, this.slides.Count - 1)], GameView);
        //    this.gameSceneManager.Start();
        //}
    }

    void LoadSlide(Type sceneType)
    {
        if (GameView.Scene is SlideSceneBase previousScene)
        {
            previousScene.Back -= OnCurrentSceneBack;
            previousScene.Next -= OnCurrentSceneNext;
        }

        this.gameSceneManager.LoadScene(sceneType, GameView);

        if (GameView.Scene is SlideSceneBase nextScene)
        {
            nextScene.Back += OnCurrentSceneBack;
            nextScene.Next += OnCurrentSceneNext;
        }

        this.gameSceneManager.Start();
    }

    private void OnCurrentSceneNext(SlideSceneBase sender)
    {
        var nextSceneIndex = this.slides.IndexOf(sender.GetType()) + 1;

        if (nextSceneIndex < this.slides.Count)
        {
            this.LoadSlide(this.slides[nextSceneIndex]);
        }
    }

    private void OnCurrentSceneBack(SlideSceneBase sender)
    {
        var previousSceneIndex = this.slides.IndexOf(GameView.Scene.GetType()) - 1;

        if (previousSceneIndex >= 0)
        {
            this.LoadSlide(this.slides[previousSceneIndex]);
        }
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        if (GameView.Scene is SlideSceneBase slideSceneBase &&
            slideSceneBase.CanProgress)
        {
            slideSceneBase.Progress();
        }
    }
}
