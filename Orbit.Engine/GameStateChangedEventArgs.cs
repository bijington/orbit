namespace Orbit.Engine;

public class GameStateChangedEventArgs : EventArgs
{
	public GameStateChangedEventArgs(GameState state)
	{
        State = state;
    }

    public GameState State { get; }
}
