using BuildingGames.Scenes;
using Orbit.Engine;

namespace BuildingGames;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly ControllerManager controllerManager;

    public IList<Type> slides = new List<Type>()
    {
        typeof(TitleScene),
        typeof(CharacterSelectionScene),
        typeof(GraphicsScene),
        typeof(HandlingInputScene),
        typeof(GameControllerScene),
        typeof(GameControllerPartTwoScene),
        typeof(MainScene),
        typeof(FreeMainScene),
        typeof(CreditsScene),
    };

    public MainPage(
        IGameSceneManager gameSceneManager,
        ControllerManager controllerManager)
	{
		InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.controllerManager = controllerManager;

        gameSceneManager.LoadScene(this.slides.First(), GameView);
        gameSceneManager.Start();

#if MACCATALYST
        this.controllerManager.Initialise();
        this.controllerManager.ButtonPressed += ControllerManager_ButtonPressed;
#endif
    }

    private void ControllerManager_ButtonPressed(ControllerButton controllerButton)
    {
        if (controllerButton == ControllerButton.NavigateForward &&
            GameView.Scene is SlideSceneBase slideSceneBase &&
            slideSceneBase.CanProgress)
        {
            var nextSceneIndex = this.slides.IndexOf(GameView.Scene.GetType()) + 1;

            this.gameSceneManager.LoadScene(this.slides[Math.Clamp(nextSceneIndex, 0, this.slides.Count - 1)], GameView);
            this.gameSceneManager.Start();
        }
        else if (controllerButton == ControllerButton.NavigateBackward)
        {
            var nextSceneIndex = this.slides.IndexOf(GameView.Scene.GetType()) - 1;

            this.gameSceneManager.LoadScene(this.slides[Math.Clamp(nextSceneIndex, 0, this.slides.Count - 1)], GameView);
            this.gameSceneManager.Start();
        }
    }

    void GameView_StartInteraction(System.Object sender, Microsoft.Maui.Controls.TouchEventArgs e)
    {
        this.gameSceneManager.LoadScene<CharacterSelectionScene>(GameView);
        this.gameSceneManager.Start();
    }
}
