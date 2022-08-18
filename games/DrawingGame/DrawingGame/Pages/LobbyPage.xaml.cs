using DrawingGame.Scenes;
using Orbit.Engine;

namespace DrawingGame.Pages;

public partial class LobbyPage : ContentPage
{
    private readonly DrawingManager drawingManager;

    public LobbyPage(DrawingManager drawingManager)
	{
		InitializeComponent();

		BindingContext = drawingManager;
        this.drawingManager = drawingManager;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CreatingTitleLabel.Text = $"Creating game '{drawingManager.GroupName}'";
    }

    private async void OnStartButtonClicked(System.Object sender, System.EventArgs e)
    {
        await drawingManager.StartSession("HOUSE", this.drawingManager.PlayerName);
    }
}
