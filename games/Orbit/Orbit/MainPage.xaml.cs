﻿using Orbit.Engine;
using Orbit.Scenes;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly HomeScene homeScene;
    private readonly MainScene mainScene;

    public static bool ShowBounds { get; private set; } = true;

    public static TouchMode TouchMode { get; private set; }

    public MainPage(
        IGameSceneManager gameSceneManager,
        HomeScene homeScene,
        MainScene mainScene)
    {
        InitializeComponent();

        this.homeScene = homeScene;
        this.mainScene = mainScene;

        this.gameSceneManager = gameSceneManager;
        
        gameSceneManager.StateChanged += GameSceneManager_StateChanged;
        gameSceneManager.LoadScene(homeScene, GameView);
    }

    private void GameSceneManager_StateChanged(object sender, GameStateChangedEventArgs e)
    {
        switch (e.State)
        {
            case GameState.Loaded:
                Pause.IsVisible = false;
                Play.IsVisible = true;
                PauseMenu.IsVisible = false;
                break;
            case GameState.Started:
                Play.IsVisible = false;
                Pause.IsVisible = true;
                PauseMenu.IsVisible = false;
                break;
            case GameState.Paused:
                PauseMenu.IsVisible = true;
                break;
            case GameState.GameOver:
                DisplayAlert("Game over", "", "Boo");
                break;
            default:
                break;
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
        gameSceneManager.LoadScene(mainScene, GameView);

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
