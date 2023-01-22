using Microsoft.Maui.Dispatching;

namespace Orbit.Engine;

public class GameSceneManager : IGameSceneManager
{
    public GameSceneManager(
        IDispatcher dispatcher,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.dispatcher = dispatcher;
        this.serviceScopeFactory = serviceScopeFactory;
    }

    private readonly IDispatcher dispatcher;
    private readonly IServiceScopeFactory serviceScopeFactory;
    private IServiceScope serviceScope;
    private int callbackMilliseconds = 16;
    private GameState gameState;
    private GameSceneView gameSceneView;
    private DateTime lastUpdate;

    public IGameScene CurrentScene { get; private set; }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public event EventHandler<GameStateChangedEventArgs> StateChanged;

    /// <inheritdoc />
    public void LoadScene<TScene>(GameSceneView gameSceneView)
        where TScene : IGameScene
    {
        this.serviceScope?.Dispose();

        this.serviceScope = this.serviceScopeFactory.CreateScope();

        var gameScene = this.serviceScope.ServiceProvider.GetRequiredService<TScene>();

        CurrentScene = gameScene;

        this.gameSceneView = gameSceneView;
        gameSceneView.Scene = gameScene;

        State = GameState.Loaded;
    }

    /// <inheritdoc />
    public IGameObject FindCollision(GameObject gameObject)
    {
        return CurrentScene.FindCollision(gameObject);
    }

    /// <inheritdoc />
    public void GameOver()
    {
        State = GameState.GameOver;
    }

    /// <inheritdoc />
    public void Pause()
    {
        State = GameState.Paused;
    }

    /// <inheritdoc />
    public void Start()
    {
        State = GameState.Started;
    }

    /// <inheritdoc />
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
