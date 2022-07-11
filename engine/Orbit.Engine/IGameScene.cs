namespace Orbit.Engine;

/// <summary>
/// Interface definition representing a scene or level in a game.
/// </summary>
public interface IGameScene : IDrawable, IGameObjectContainer
{
    // TODO: Move out to a collision detection manager or similar.
    GameObject FindCollision(GameObject gameObject);
}
