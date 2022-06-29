using Orbit.Engine;
using Orbit.Scenes;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly MainScene mainScene;

    public static bool ShowBounds { get; } = true;

    public static TouchMode TouchMode { get; private set; }

    public MainPage(
        IGameSceneManager gameSceneManager,
        HomeScene scene,
        MainScene mainScene)
    {
        InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        gameSceneManager.StateChanged += GameSceneManager_StateChanged;
        this.mainScene = mainScene;
        gameSceneManager.LoadScene(scene, GameView);

        gameSceneManager.Pause();
    }

    private void GameSceneManager_StateChanged(object sender, GameStateChangedEventArgs e)
    {
        if (e.State == GameState.GameOver)
        {
            DisplayAlert("Game over", "", "Boo");
        }
    }

    // TODO: GameObject
    void GameView_EndInteraction(object sender, TouchEventArgs e)
    {
        TouchMode = TouchMode.None;
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        var middle = GameView.Width / 2;

        var touchX = e.Touches.First().X;

        if (touchX >= middle)
        {
            TouchMode = TouchMode.SpeedUp;
        }
        else
        {
            TouchMode = TouchMode.SlowDown;
        }
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        if (gameSceneManager.State == GameState.Paused)
        {
            gameSceneManager.Start();
        }
        else
        {
            gameSceneManager.Pause();
        }
    }

    void PlayButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Play.IsVisible = false;
        gameSceneManager.LoadScene(mainScene, GameView);

        gameSceneManager.Start();
    }
}
