namespace Orbit.Engine;

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

    IGameObject FindCollision(GameObject gameObject);

    void GameOver();

    /// <summary>
    /// Loads the supplied <typeparamref name="TScene"/> into the supplied <paramref name="gameSceneView"/>.
    /// Note that for each scene that is loaded a new <see cref="IServiceScope"/> will be created.
    /// </summary>
    /// <typeparam name="TScene">An implementation of <see cref="IGameScene"/> to load into the supplied <paramref name="gameSceneView"/></typeparam>
    /// <param name="gameSceneView">The destination <see cref="GameSceneView"/> to display.</param>
    void LoadScene<TScene>(GameSceneView gameSceneView)
        where TScene: IGameScene;

    void Pause();
    void Start();
    void Stop();
}
