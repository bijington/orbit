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
    }

    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        if (sender is Button button)
        {
            drawingManager.SelectedColor = button.BackgroundColor;
        }

        await AnimateColorSelectionPanel();
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

        drawingManager.SetPage(this);

        gameSceneManager.LoadScene(mainScene, SceneView);
        gameSceneManager.Start();

        this.ColorSelection.IsVisible = this.drawingManager.IsDrawing;
        this.WordLabel.IsVisible = this.drawingManager.IsDrawing;
        this.GuessingPanel.IsVisible = this.drawingManager.IsViewing;
        this.GuessingTitlePanel.IsVisible = this.drawingManager.IsViewing;
    }

    async void OnColorSelectionButtonClicked(System.Object sender, System.EventArgs e)
    {
        await AnimateColorSelectionPanel();
    }

    async void OnGuessButtonClicked(System.Object sender, System.EventArgs e)
    {
        if (this.GuessedWord.Text != this.drawingManager.Word)
        {
            Animation a = new Animation();

            a.Add(0, 0.125, new Animation(v => this.GuessedWord.TranslationX = v, 0, -13));
            a.Add(0.125, 0.250, new Animation(v => this.GuessedWord.TranslationX = v, -13, 13));
            a.Add(0.250, 0.375, new Animation(v => this.GuessedWord.TranslationX = v, 13, -11));
            a.Add(0.375, 0.5, new Animation(v => this.GuessedWord.TranslationX = v, -11, 11));
            a.Add(0.5, 0.625, new Animation(v => this.GuessedWord.TranslationX = v, 11, -7));
            a.Add(0.625, 0.75, new Animation(v => this.GuessedWord.TranslationX = v, -7, 7));
            a.Add(0.75, 0.875, new Animation(v => this.GuessedWord.TranslationX = v, 7, -5));
            a.Add(0.875, 1, new Animation(v => this.GuessedWord.TranslationX = v, -5, 0));

            a.Commit(
                owner: this.GuessedWord,
                name: "WrongGuess",
                length: 500,
                easing: Easing.Linear, finished: (x, y) => { });

            return;
        }

        await drawingManager.PerformGuess(this.GuessedWord.Text);
    }

    void GuessedWord_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        
    }

    private async Task AnimateColorSelectionPanel()
    {
        if (ColorSelection.TranslationY == 60)
        {
            await Task.WhenAll(
                ColorSelection.TranslateTo(0, -320),
                ColorSelection.FadeTo(0));
        }
        else
        {
            await Task.WhenAll(
                ColorSelection.TranslateTo(0, 60),
                ColorSelection.FadeTo(1));
        }
    }
}
