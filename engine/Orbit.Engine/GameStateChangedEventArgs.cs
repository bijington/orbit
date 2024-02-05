namespace Orbit.Engine;

/// <summary>
/// Event arguments containing information about the current <see cref="GameState"/> for the currently loaded <see cref="GameScene"/>.
/// </summary>
public class GameStateChangedEventArgs : EventArgs
{
    /// <summary>
    /// Creates a new instance of <see cref="GameStateChangedEventArgs"/>.
    /// </summary>
    /// <param name="state">The current <see cref="GameState"/> for the currently loaded <see cref="GameScene"/>.</param>
	public GameStateChangedEventArgs(GameState state)
	{
        State = state;
    }

    /// <summary>
    /// Gets the current <see cref="GameState"/> for the currently loaded <see cref="GameScene"/>.
    /// </summary>
    public GameState State { get; }
}
