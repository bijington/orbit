namespace Orbit.Engine;

/// <summary>
/// Interface definition representing an implementation capable of managing the state around a <see cref="GameScene"/>.
/// </summary>
public interface IGameSceneManager
{
    /// <summary>
    /// Gets the state of the current <see cref="IGameScene"/>.
    /// </summary>
    GameState State { get; }

    /// <summary>
    /// Event raised when the <see cref="State"/> property changes.
    /// </summary>
    event EventHandler<GameStateChangedEventArgs> StateChanged;

    /// <summary>
    /// Moves the currently loaded <see cref="GameScene"/> into a completed state.
    /// Transitions the current scene into the <see cref="GameState.Completed"/>.
    /// </summary>
    void Complete();

    /// <summary>
    /// Moves the currently loaded <see cref="GameScene"/> into a failed state.
    /// Transitions the current scene into the <see cref="GameState.GameOver"/>.
    /// </summary>
    void GameOver();

    /// <summary>
    /// Loads the supplied <typeparamref name="TScene"/> into the supplied <paramref name="gameSceneView"/>.
    /// Note that for each scene that is loaded a new <see cref="IServiceScope"/> will be created.
    /// </summary>
    /// <typeparam name="TScene">An implementation of <see cref="IGameScene"/> to load into the supplied <paramref name="gameSceneView"/>.</typeparam>
    /// <param name="gameSceneView">The destination <see cref="GameSceneView"/> to display.</param>
    void LoadScene<TScene>(GameSceneView gameSceneView)
        where TScene: IGameScene;

    /// <summary>
    /// Loads the supplied <paramref name="sceneType"/> into the supplied <paramref name="gameSceneView"/>.
    /// Note that for each scene that is loaded a new <see cref="IServiceScope"/> will be created.
    /// </summary>
    /// <param name="sceneType">An implementation of <see cref="IGameScene"/> to load into the supplied <paramref name="gameSceneView"/>.</param>
    /// <param name="gameSceneView">The destination <see cref="GameSceneView"/> to display.</param>
    void LoadScene(Type sceneType, GameSceneView gameSceneView);

    /// <summary>
    /// Starts the currently running <see cref="GameScene"/>.
    /// Transitions the current scene into the <see cref="GameState.Paused"/>.
    /// </summary>
    void Pause();

    /// <summary>
    /// Starts the currently loaded <see cref="GameScene"/>.
    /// Transitions the current scene into the <see cref="GameState.Started"/>.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops the currently running <see cref="GameScene"/>.
    /// Transitions the current scene into the <see cref="GameState.Loaded"/>.
    /// </summary>
    void Stop();
}
