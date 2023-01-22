using Orbit.Engine;
using Orbit.Scenes;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly UserInputManager userInputManager;

    public static bool ShowBounds { get; private set; } = false;

    public MainPage(
        IGameSceneManager gameSceneManager,
        UserInputManager userInputManager)
    {
        InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.userInputManager = userInputManager;
        gameSceneManager.StateChanged += GameSceneManager_StateChanged;
        gameSceneManager.LoadScene<HomeScene>(GameView);
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

                gameSceneManager.LoadScene<HomeScene>(GameView);
                break;

            default:
                break;
        }
    }

    void GameView_EndInteraction(object sender, TouchEventArgs e)
    {
        userInputManager.FinishTouch();
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
        userInputManager.HandleTouch(e.Touches.First().X, GameView.Width);
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
        gameSceneManager.LoadScene<MainScene>(GameView);

        gameSceneManager.Start();
    }

    void OnResumeButtonClicked(System.Object sender, System.EventArgs e)
    {
        gameSceneManager.Start();
    }

    void OnQuitButtonClicked(System.Object sender, System.EventArgs e)
    {
        gameSceneManager.LoadScene<HomeScene>(GameView);
    }

    void Switch_Toggled(System.Object sender, Microsoft.Maui.Controls.ToggledEventArgs e)
    {
        ShowBounds = e.Value;
    }
}
