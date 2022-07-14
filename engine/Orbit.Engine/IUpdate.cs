namespace Orbit.Engine;

/// <summary>
/// Interface definition representing something that can have it's state updated.
/// </summary>
public interface IUpdate
{
    /// <summary>
    /// Updates this implementation as part of the game loop.
    /// </summary>
    /// <remarks>
    /// When the <see cref="GameState"/> is <see cref="GameState.Started"/> then expect to be called on each frame.
    /// </remarks>
    /// <param name="millisecondsSinceLastUpdate">The number of milliseconds since update was last called.</param>
    void Update(double millisecondsSinceLastUpdate);
}
