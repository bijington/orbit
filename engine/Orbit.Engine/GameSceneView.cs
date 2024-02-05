namespace Orbit.Engine;

/// <summary>
/// A <see cref="View"/> that can be added to your applications UI and enables the ability to render
/// a <see cref="GameScene"/>.
/// Provides full touch/mouse support through use of the <see cref="GraphicsView"/> interaction events.
/// </summary>
public class GameSceneView : GraphicsView
{
    /// <summary>
    /// Creates a new instance of <see cref="GameSceneView"/>.
    /// </summary>
    public GameSceneView()
    {
        BackgroundColor = Colors.Transparent;
    }

    private IGameScene scene;

    /// <summary>
    /// Gets the currently loaded <see cref="IGameScene"/> implementation.
    /// </summary>
    public IGameScene Scene
    {
        get => scene;
        internal set
        {
            scene = value;
            Drawable = value;
        }
    }
}
