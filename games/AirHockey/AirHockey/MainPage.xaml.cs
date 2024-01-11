using System.Diagnostics;
using AirHockey.Scenes;
using Orbit.Engine;

namespace AirHockey;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private PlayerStateManager playerStateManager;

	public MainPage(
        IGameSceneManager gameSceneManager,
        PlayerStateManager playerStateManager)
	{
		InitializeComponent();

        this.playerStateManager = playerStateManager;
        this.gameSceneManager = gameSceneManager;

        gameSceneManager.LoadScene<MainScene>(GameView);
    }

    private async void OnPlayButtonClicked(object sender, EventArgs eventArgs)
    {
        try 
        {
            await playerStateManager.Connect();

            gameSceneManager.Start();

            PlayButton.IsVisible = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());

            await DisplayAlert("Error playing game", ex.ToString(), "OK");
        }
    }

    void GameView_EndInteraction(object sender, TouchEventArgs e)
    {
    }

    void GameView_StartInteraction(object sender, TouchEventArgs e)
    {
    }

    void GameView_DragInteraction(System.Object sender, Microsoft.Maui.Controls.TouchEventArgs e)
    {
        var touch = e.Touches.First();

        var bounds = GameView.Bounds;

        var relativeY = touch.Y / bounds.Height;
        var relativeX = touch.X / bounds.Width;

        _ = playerStateManager.UpdateState((float)relativeX, (float)relativeY);

        this.Debugging.Text = 
        $"{this.playerStateManager.PlayerState.Id} vs {this.playerStateManager.OpponentState.Id}";
    }
}
