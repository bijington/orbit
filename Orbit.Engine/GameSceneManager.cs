using Microsoft.Maui.Dispatching;

namespace Orbit.Engine;

public class GameSceneManager : IGameSceneManager
{
    private readonly IDispatcher dispatcher;
    private int callbackMilliseconds = 16;
    private GameState gameState;
    private GameSceneView gameSceneView;

    public GameState State
    {
        get => gameState;
        set
        {
            gameState = value;
            UpdateScene();
            StateChanged?.Invoke(this, new GameStateChangedEventArgs(value));
        }
    }

    public GameSceneManager(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    public IGameScene CurrentScene { get; private set; }

    public event EventHandler<GameStateChangedEventArgs> StateChanged;

    public void LoadScene(IGameScene gameScene, GameSceneView graphicsView)
    {
        CurrentScene = gameScene;

        gameSceneView = graphicsView;
        gameSceneView.Scene = gameScene;
        gameSceneView.Drawable = gameScene;
    }

    public GameObject FindCollision(GameObject gameObject)
    {
        return CurrentScene.FindCollision(gameObject);
    }

    public void GameOver()
    {
        State = GameState.GameOver;
    }

    public void Pause()
    {
        State = GameState.Paused;
    }

    public void Start()
    {
        State = GameState.Started;
    }

    public void Stop()
    {
        State = GameState.Loaded;
    }

    private void UpdateScene()
    {
        if (gameState != GameState.Started)
        {
            return;
        }

        CurrentScene.Update();

        gameSceneView.Invalidate();

        dispatcher.DispatchDelayed(
            TimeSpan.FromMilliseconds(callbackMilliseconds),
            () =>
            {
                UpdateScene();
            });
    }
}
