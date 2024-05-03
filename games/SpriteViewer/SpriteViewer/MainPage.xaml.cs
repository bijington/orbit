using Orbit.Engine;
using Orbit.Scenes;

namespace SpriteViewer;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;

    public MainPage(
        IGameSceneManager gameSceneManager)
    {
        InitializeComponent();

        this.gameSceneManager = gameSceneManager;

        gameSceneManager.LoadScene<SpriteViewerScene>(GameView);
    }
}
