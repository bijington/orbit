namespace Orbit.Engine;

/// <summary>
/// Interface definition representing something that can be rendered on screen.
/// </summary>
public interface IRender
{
    /// <summary>
    /// Provides this implementation with the ability to render anything it needs to on screen.
    /// </summary>
    /// <remarks>
    /// Consider this method the process of converting between your state which should be updated in <see cref="IUpdate.Update"/>
    /// and the actual display on screen for the user to see.
    /// </remarks>
    /// <param name="canvas">The <see cref="ICanvas"/> implementation to render on.</param>
    /// <param name="dimensions">The dimensions of the canvas, this allows for calculating where exactly you wish to render.</param>
    void Render(ICanvas canvas, RectF dimensions);
}
