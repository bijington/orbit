using System.Diagnostics;
using AirHockey.Scenes;
using Orbit.Engine;

namespace AirHockey;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly PlayerStateManager playerStateManager;

	public MainPage(
        IGameSceneManager gameSceneManager,
        PlayerStateManager playerStateManager)
	{
		InitializeComponent();

        this.playerStateManager = playerStateManager;
        this.gameSceneManager = gameSceneManager;

        gameSceneManager.LoadScene<MainScene>(GameView);
    }

    private async void OnOnlineButtonClicked(object sender, EventArgs eventArgs)
    {
        try 
        {
            await playerStateManager.Initialise();
            await playerStateManager.Connect();

            gameSceneManager.Start();

            ButtonPanel.IsVisible = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());

            await DisplayAlert("Error playing game", ex.ToString(), "OK");
        }
    }

    private async void OnOfflineButtonClicked(object sender, EventArgs eventArgs)
    {
        try 
        {
            // Need to split between PlayerStateManager and GameManager...
            await playerStateManager.Initialise();
            //await playerStateManager.Connect();

            gameSceneManager.Start();

            ButtonPanel.IsVisible = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());

            await DisplayAlert("Error playing game", ex.ToString(), "OK");
        }
    }

    void GameView_DragInteraction(object sender, TouchEventArgs e)
    {
        var touch = e.Touches.First();

        var bounds = GameView.Bounds;

        var relativeY = touch.Y / bounds.Height;
        var relativeX = touch.X / bounds.Width;

        _ = playerStateManager.UpdateState((float)relativeX, (float)relativeY);

        this.Debugging.Text = 
        $"{this.playerStateManager.PlayerState.Id} vs {this.playerStateManager.OpponentState?.Id}";
    }
}
