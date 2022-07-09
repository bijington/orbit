using Microsoft.Maui.Dispatching;

namespace Orbit.Engine;

public class GameSceneManager : IGameSceneManager
{
    public GameSceneManager(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    private readonly IDispatcher dispatcher;
    private int callbackMilliseconds = 16;
    private GameState gameState;
    private GameSceneView gameSceneView;
    private DateTime lastUpdate;

    public IGameScene CurrentScene { get; private set; }

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

    public event EventHandler<GameStateChangedEventArgs> StateChanged;

    public void LoadScene(IGameScene gameScene, GameSceneView gameSceneView)
    {
        CurrentScene = gameScene;

        this.gameSceneView = gameSceneView;
        gameSceneView.Scene = gameScene;

        State = GameState.Loaded;
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

        var currentUpdate = DateTime.UtcNow;
        var timeSinceLastUpdate = currentUpdate - lastUpdate;

        lastUpdate = currentUpdate;

        CurrentScene.Update(timeSinceLastUpdate.TotalMilliseconds);

        gameSceneView.Invalidate();

        var postUpdate = DateTime.UtcNow;
        var updateDuration = callbackMilliseconds - (postUpdate - currentUpdate).TotalMilliseconds;

        var delayUntilNextUpdate = Math.Min(updateDuration, callbackMilliseconds);

        dispatcher.DispatchDelayed(
            TimeSpan.FromMilliseconds(delayUntilNextUpdate),
            () =>
            {
                UpdateScene();
            });
    }
}
