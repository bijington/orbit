using Orbit.Engine;
using Orbit.Scenes;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly HomeScene homeScene;
    private readonly IServiceProvider serviceProvider;

    public static bool ShowBounds { get; private set; } = false;

    public static TouchMode TouchMode { get; private set; }

    public MainPage(
        IGameSceneManager gameSceneManager,
        HomeScene homeScene,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();

        this.homeScene = homeScene;
        this.serviceProvider = serviceProvider;

        this.gameSceneManager = gameSceneManager;
        
        gameSceneManager.StateChanged += GameSceneManager_StateChanged;
        gameSceneManager.LoadScene(homeScene, GameView);
    }

    private async void GameSceneManager_StateChanged(object sender, GameStateChangedEventArgs e)
    {
        switch (e.State)
        {
            case GameState.Loaded:
                Pause.IsVisible = false;
                PauseMenu.IsVisible = false;
                Play.IsVisible = true;
                TitleLabel.IsVisible = true;
                break;

            case GameState.Started:
                await Task.WhenAll(
                    Play.ScaleTo(0, 250, Easing.SinOut),
                    TitleLabel.FadeTo(0, 500, Easing.SinOut));

                Pause.IsVisible = true;
                PauseMenu.IsVisible = false;
                Play.IsVisible = false;
                TitleLabel.IsVisible = false;

                Play.Scale = 1;
                TitleLabel.Opacity = 1;
                break;

            case GameState.Paused:
                PauseMenu.IsVisible = true;
                TitleLabel.IsVisible = false;
                break;

            case GameState.GameOver:
                await DisplayAlert("Game over", "", "Boo");

                gameSceneManager.LoadScene(homeScene, GameView);
                break;

            default:
                break;
        }
    }

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
        gameSceneManager.LoadScene(serviceProvider.GetRequiredService<MainScene>(), GameView);

        gameSceneManager.Start();
    }

    void OnResumeButtonClicked(System.Object sender, System.EventArgs e)
    {
        gameSceneManager.Start();
    }

    void OnQuitButtonClicked(System.Object sender, System.EventArgs e)
    {
        gameSceneManager.LoadScene(homeScene, GameView);
    }

    void Switch_Toggled(System.Object sender, Microsoft.Maui.Controls.ToggledEventArgs e)
    {
        ShowBounds = e.Value;
    }
}
