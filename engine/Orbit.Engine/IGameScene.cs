namespace Orbit.Engine;

/// <summary>
/// Interface definition representing a scene or level in a game.
/// </summary>
public interface IGameScene : IDrawable
{
    /// <summary>
    /// Adds the supplied <paramref name="gameObject"/> to this <see cref="IGameScene"/>.
    /// This will include it for updates when the game is running.
    /// </summary>
    /// <param name="gameObject">The new <see cref="GameObject"/> to add to the scene.</param>
    void Add(GameObject gameObject);

    /// <summary>
    /// Removes the supplied <paramref name="gameObject"/> from this <see cref="IGameScene"/>.
    /// </summary>
    /// <param name="gameObject">The existing <see cref="GameObject"/> to remove from the scene.</param>
    void Remove(GameObject gameObject);

    GameObject FindCollision(GameObject gameObject);

    /// <summary>
    /// Provides the <see cref="IGameScene"/> the ability to render anything it needs to on screen.
    /// </summary>
    /// <remarks>
    /// Consider this method the process of converting between your state which should be updated in <see cref="Update"/>
    /// and the actual display on screen for the user to see.
    /// </remarks>
    /// <param name="canvas">The <see cref="ICanvas"/> implementation to render on.</param>
    /// <param name="dimensions">The dimensions of the canvas, this allows for calculating where exactly you wish to render.</param>
    void Render(ICanvas canvas, RectF dimensions);

    /// <summary>
    /// Updates the <see cref="IGameScene"/> as part of the game loop.
    /// </summary>
    /// <remarks>
    /// When the <see cref="GameState"/> is <see cref="GameState.Started"/> then expect to be called on each frame.
    /// </remarks>
    /// <param name="millisecondsSinceLastUpdate">The number of milliseconds since update was last called.</param>
    void Update(double millisecondsSinceLastUpdate);
}
