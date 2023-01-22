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
    /// Loads the supplied <paramref name="gameScene"/> into the supplied <paramref name="gameSceneView"/>.
    /// </summary>
    /// <param name="gameScene">The <see cref="IGameScene"/> implementation to load.</param>
    /// <param name="gameSceneView">The destination <see cref="GameSceneView"/> to display the supplied <paramref name="gameScene"/>.</param>
    void LoadScene(IGameScene gameScene, GameSceneView gameSceneView);

    void Pause();
    void Start();
    void Stop();
}
