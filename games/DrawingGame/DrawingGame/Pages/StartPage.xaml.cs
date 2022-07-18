using DrawingGame.Scenes;
using Orbit.Engine;

namespace DrawingGame.Pages;

public partial class StartPage : ContentPage
{
    private readonly DrawingManager drawingManager;

    public StartPage(DrawingManager drawingManager)
	{
		InitializeComponent();
        this.drawingManager = drawingManager;
    }

    private async void OnCreateButtonClicked(System.Object sender, System.EventArgs e)
    {
        // TODO: This would really be server driven.
        drawingManager.IsPrimary = true;

        await drawingManager.StartGame(PlayerNameField.Text, GroupNameField.Text, true);

        await Shell.Current.GoToAsync("lobby");
    }

    private async void OnJoinButtonClicked(System.Object sender, System.EventArgs e)
    {
        drawingManager.IsPrimary = false;

        await drawingManager.StartGame(PlayerNameField.Text, GroupNameField.Text, false);

        await Shell.Current.GoToAsync("lobby");
    }
}
