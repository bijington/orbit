namespace Orbit.Engine;

public class GameSceneView : GraphicsView
{
    public GameSceneView()
    {
        BackgroundColor = Colors.Transparent;
    }

    private IGameScene scene;

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
