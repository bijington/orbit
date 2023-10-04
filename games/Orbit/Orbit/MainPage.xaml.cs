using Orbit.Audio;
using Orbit.Engine;
using Orbit.Scenes;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly UserInputManager userInputManager;
    private readonly AudioService audioService;
    private readonly IDeviceDisplay deviceDisplay;
    private readonly SettingsManager settingsManager;

    public MainPage(
        IGameSceneManager gameSceneManager,
        UserInputManager userInputManager,
        AudioService audioService,
        IDeviceDisplay deviceDisplay,
        SettingsManager settingsManager)
    {
        InitializeComponent();

        this.gameSceneManager = gameSceneManager;
        this.userInputManager = userInputManager;
        this.audioService = audioService;
        this.deviceDisplay = deviceDisplay;
        this.settingsManager = settingsManager;
        gameSceneManager.StateChanged += OnGameSceneManagerStateChanged;
        gameSceneManager.LoadScene<HomeScene>(GameView);
    }

    private async void OnGameSceneManagerStateChanged(object sender, GameStateChangedEventArgs e)
    {
        this.deviceDisplay.KeepScreenOn = false;

        switch (e.State)
        {
            case GameState.Loaded:
                Pause.IsVisible = false;
                PauseMenu.IsVisible = false;
                Play.IsVisible = true;
                TitleLabel.IsVisible = true;

                this.audioService.Stop(AudioItem.Music.Main);
                await this.audioService.Play(AudioItem.Music.HomeBackground, true);
                break;

            case GameState.Started:
                this.deviceDisplay.KeepScreenOn = true;

                await Task.WhenAll(
                    Play.ScaleTo(0, 250, Easing.SinOut),
                    TitleLabel.FadeTo(0, 500, Easing.SinOut));

                Pause.IsVisible = true;
                PauseMenu.IsVisible = false;
                Play.IsVisible = false;
                TitleLabel.IsVisible = false;

                Play.Scale = 1;
                TitleLabel.Opacity = 1;

                this.audioService.Stop(AudioItem.Music.HomeBackground);
                await this.audioService.Play(AudioItem.Music.Main, true);
                break;

            case GameState.Paused:
                PauseMenu.IsVisible = true;
                TitleLabel.IsVisible = false;

                this.audioService.Stop(AudioItem.Music.Main);
                await this.audioService.Play(AudioItem.Music.HomeBackground, true);
                break;

            case GameState.GameOver:
                await DisplayAlert("Game over", "", "Boo");

                gameSceneManager.LoadScene<HomeScene>(GameView);
                break;

            default:
                break;
        }
    }

    void OnGameViewEndInteraction(object sender, TouchEventArgs e)
    {
        userInputManager.FinishTouch();
    }

    void OnGameViewStartInteraction(object sender, TouchEventArgs e)
    {
        userInputManager.HandleTouch(e.Touches.First().X, GameView.Width);
    }

    void OnPauseButtonClicked(object sender, EventArgs e)
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

    void OnPlayButtonClicked(object sender, EventArgs e)
    {
        gameSceneManager.LoadScene<MainScene>(GameView);

        gameSceneManager.Start();
    }

    void OnSlowDownButtonPressed(object sender, EventArgs e)
    {
        userInputManager.SetTouchMode(TouchMode.SlowDown);
    }

    void OnSlowDownButtonReleased(object sender, EventArgs e)
    {
        userInputManager.SetTouchMode(TouchMode.None);
    }

    void OnSpeedUpButtonPressed(object sender, EventArgs e)
    {
        userInputManager.SetTouchMode(TouchMode.SpeedUp);
    }

    void OnSpeedUpButtonReleased(object sender, EventArgs e)
    {
        userInputManager.SetTouchMode(TouchMode.None);
    }

    void OnResumeButtonClicked(object sender, EventArgs e)
    {
        gameSceneManager.Start();
    }

    void OnQuitButtonClicked(object sender, EventArgs e)
    {
        gameSceneManager.LoadScene<HomeScene>(GameView);
    }

    void OnDebugSwitchToggled(object sender, ToggledEventArgs e)
    {
        settingsManager.ShowDebug = e.Value;
    }

    void OnShowButtonsSwitchToggled(object sender, ToggledEventArgs e)
    {
        userInputManager.SetInputMode(e.Value ? UserInputMode.Buttons : UserInputMode.TouchOnScreen);

        settingsManager.ShowControls = e.Value;

        ButtonPanel.IsVisible = e.Value;
    }
}
