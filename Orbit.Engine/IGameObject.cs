namespace Orbit.Engine;

/// <summary>
/// Interface definition representing an object in a game.
/// </summary>
public interface IGameObject
{
    RectF Bounds { get; }

    /// <summary>
    /// Provides the <see cref="IGameObject"/> the ability to render anything it needs to on screen.
    /// </summary>
    /// <remarks>
    /// Consider this method the process of converting between your state which should be updated in <see cref="Update"/>
    /// and the actual display on screen for the user to see.
    /// </remarks>
    /// <param name="canvas">The <see cref="ICanvas"/> implementation to render on.</param>
    /// <param name="dimensions">The dimensions of the canvas, this allows for calculating where exactly you wish to render.</param>
    void Render(ICanvas canvas, RectF dimensions);

    /// <summary>
    /// Updates the <see cref="IGameObject"/> as part of the game loop.
    /// Use this to update your objects state ready for when the <see cref="Render"/> method is called.
    /// </summary>
    /// <remarks>
    /// When the <see cref="GameState"/> is <see cref="GameState.Started"/> then expect to be called on each frame.
    /// </remarks>
    /// <param name="millisecondsSinceLastUpdate">The number of milliseconds since update was last called.</param>
    void Update(double millisecondsSinceLastUpdate);
}
