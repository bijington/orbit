namespace Orbit.Engine;

/// <summary>
/// Interface definition representing an object in a game.
/// </summary>
public interface IGameObject : IRender, IUpdate
{
    /// <summary>
    /// Gets the bounds of the <see cref="IGameObject"/>.
    /// </summary>
    RectF Bounds { get; } // TODO: Should this just be a flat set of primitives properties to save on allocations?

    /// <summary>
    /// Gets or sets the current <see cref="GameScene"/> that the <see cref="IGameObject"/> is a part of.
    /// </summary>
    GameScene CurrentScene { get; set; }
}
