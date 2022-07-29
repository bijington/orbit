using DrawingGame.Scenes;
using Orbit.Engine;

namespace DrawingGame.Pages;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly MainScene mainScene;
    private readonly DrawingManager drawingManager;

    public MainPage(
		IGameSceneManager gameSceneManager,
		MainScene mainScene,
        DrawingManager drawingManager)
	{
		InitializeComponent();

        BindingContext = drawingManager;

        this.gameSceneManager = gameSceneManager;
        this.mainScene = mainScene;
        this.drawingManager = drawingManager;

        if (this.drawingManager.IsViewing)
        {
            this.ColorSelection.IsVisible = false;
            this.WordLabel.IsVisible = false;
        }
    }

    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        if (sender is Button button)
        {
            drawingManager.SelectedColor = button.BackgroundColor;
        }

        await ColorSelection.TranslateTo(0, -320);
    }

    private void OnSceneViewDragInteraction(object sender, Microsoft.Maui.Controls.TouchEventArgs e)
    {
        drawingManager.UpdateDrawing(e.Touches.First());
    }

    private void OnSceneViewEndInteraction(object sender, Microsoft.Maui.Controls.TouchEventArgs e)
    {
        drawingManager.EndDrawing(e.Touches.First());
    }

    private void OnSceneViewStartInteraction(object sender, Microsoft.Maui.Controls.TouchEventArgs e)
    {
        drawingManager.StartDrawing(e.Touches.First());
    }

    private void OnClearButtonClicked(System.Object sender, System.EventArgs e)
    {
        drawingManager.Clear();
    }

    private void OnLineThicknessValueChanged(object sender, ValueChangedEventArgs e)
    {
        drawingManager.LineThickness = (float)e.NewValue;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        gameSceneManager.LoadScene(mainScene, SceneView);
        gameSceneManager.Start();
    }

    async void OnColorSelectionButtonClicked(System.Object sender, System.EventArgs e)
    {
        if (ColorSelection.TranslationY == 60)
        {
            await ColorSelection.TranslateTo(0, -320);
        }
        else
        {
            await ColorSelection.TranslateTo(0, 60);
        }
    }
}
