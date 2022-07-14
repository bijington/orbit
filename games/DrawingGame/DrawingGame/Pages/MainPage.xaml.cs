using DrawingGame.Scenes;
using Orbit.Engine;

namespace DrawingGame.Pages;

public partial class MainPage : ContentPage
{
    private readonly DrawingManager drawingManager;

    public MainPage(
		IGameSceneManager gameSceneManager,
		MainScene mainScene,
        DrawingManager drawingManager)
	{
		InitializeComponent();

		gameSceneManager.LoadScene(mainScene, SceneView);
        gameSceneManager.Start();

        this.drawingManager = drawingManager;
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        if (sender is Button button)
        {
            drawingManager.SelectedColor = button.BackgroundColor;

            Preview.Fill = new SolidColorBrush(button.BackgroundColor);
        }
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
}
