namespace Orbit.Engine;

/// <summary>
/// Enumeration of the possible states that a game can be in.
/// </summary>
public enum GameState
{
    /// <summary>
    /// No game exists. This is the default state.
    /// </summary>
    Empty = 0,

    /// <summary>
    /// The game has loaded. You will need to call <see cref="IGameSceneManager.Start"/> in order to progress.
    /// Triggered by calling <see cref="IGameSceneManager.LoadScene"/>.
    /// </summary>
    Loaded = 1,

    /// <summary>
    /// The game has started.
    /// Triggered by calling <see cref="IGameSceneManager.Start"/>.
    /// </summary>
    Started = 2,

    /// <summary>
    /// The game is paused.
    /// Triggered by calling <see cref="IGameSceneManager.Pause"/>.
    /// </summary>
    Paused = 3,

    /// <summary>
    /// The game is in a game over state. Use this to determine whether the player has failed to complete a level.
    /// Triggered by calling <see cref="IGameSceneManager.GameOver"/>.
    /// </summary>
    GameOver = 4,

    /// <summary>
    /// The game is complete. Use this to determine that the player has completed the current level.
    /// Triggered by calling <see cref="IGameSceneManager.Complete"/>.
    /// </summary>
    Completed = 5
}
