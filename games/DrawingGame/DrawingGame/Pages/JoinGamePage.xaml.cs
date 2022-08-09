namespace DrawingGame.Pages;

public partial class JoinGamePage : ContentPage
{
    private readonly DrawingManager drawingManager;

    public JoinGamePage(DrawingManager drawingManager)
    {
		InitializeComponent();
        this.drawingManager = drawingManager;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        JoiningTitleLabel.Text = $"Joining game '{drawingManager.GroupName}'";
    }
}
