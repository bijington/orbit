namespace Orbit.Engine;

/// <summary>
/// Interface definition representing an object in a game.
/// </summary>
public interface IGameObject : IRender, IUpdate
{
    RectF Bounds { get; }

    GameScene CurrentScene { get; set; }

    bool IsCollisionDetectionEnabled { get; }
}
