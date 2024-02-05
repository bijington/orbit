namespace Orbit.Engine;

/// <summary>
/// Interface definition representing an object in a game.
/// </summary>
public interface IGameObject : IRender, IUpdate
{
    RectF Bounds { get; } // TODO: Should this just be a flat set of primitives properties to save on allocations?

    GameScene CurrentScene { get; set; }
}
