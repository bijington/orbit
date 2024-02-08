namespace Orbit.Engine;

/// <summary>
/// Interface definition for anything that can contain multiple <see cref="GameObject"/>s.
/// Two key examples of this is the <see cref="GameScene"/> and <see cref="GameObject"/> implementations.
/// </summary>
public interface IGameObjectContainer : IRender, IUpdate
{
    /// <summary>
    /// Adds the supplied <paramref name="gameObject"/> to this <see cref="IGameObjectContainer"/>.
    /// This will include it for updates when the game is running.
    /// </summary>
    /// <param name="gameObject">The new <see cref="GameObject"/> to add to the scene.</param>
    void Add(GameObject gameObject);

    /// <summary>
    /// Removes the supplied <paramref name="gameObject"/> from this <see cref="IGameObjectContainer"/>.
    /// </summary>
    /// <param name="gameObject">The existing <see cref="GameObject"/> to remove from the scene.</param>
    void Remove(GameObject gameObject);
}
