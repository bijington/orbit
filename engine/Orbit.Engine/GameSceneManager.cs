namespace Orbit.Engine;

/// <summary>
/// Implementation of the <see cref="IGameSceneManager"/> interface, enabling support for the management of the life of a <see cref="GameScene"/>.
/// </summary>
public class GameSceneManager : IGameSceneManager
{
    /// <summary>
    /// Creates a new instance of <see cref="GameSceneManager"/>.
    /// </summary>
    /// <param name="dispatcher">The <see cref="IDispatcher"/> implementation, responsible for managing the game engine looping mechanism.</param>
    /// <param name="serviceScopeFactory">The <see cref="IServiceScopeFactory"/> implementation, responsible for enabling the ability to manage scoped service lifetimes.</param>
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
    private double callbackMilliseconds = 16;
    private double frameRate = 62.5;
    private GameState gameState;
    private GameSceneView gameSceneView;
    private DateTime lastUpdate;

    /// <summary>
    /// Gets the currently loaded <see cref="GameScene"/>.
    /// </summary>
    public IGameScene CurrentScene { get; private set; }

    /// <summary>
    /// Gets the current frame rate in frames per second.
    /// </summary>
    public double FrameRate
    {
        get => frameRate;
        set
        {
            frameRate = value;
            callbackMilliseconds = value != 0 ? 1000d / value : 0;// TODO: Is this dangerous???
        }
    }

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
        LoadScene(typeof(TScene), gameSceneView);
    }

    /// <inheritdoc />
    public void LoadScene(Type sceneType, GameSceneView gameSceneView)
    {
        this.serviceScope?.Dispose();

        this.serviceScope = this.serviceScopeFactory.CreateScope();

        var gameScene = (GameScene)this.serviceScope.ServiceProvider.GetRequiredService(sceneType);

        CurrentScene = gameScene;

        this.gameSceneView = gameSceneView;
        gameSceneView.Scene = gameScene;

        State = GameState.Loaded;
    }

    /// <inheritdoc />
    public void Complete()
    {
        State = GameState.Completed;
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
        lastUpdate = DateTime.UtcNow;
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
