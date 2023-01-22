namespace Orbit.Engine;

/// <summary>
/// Base class definition representing a scene or level in a game.
/// </summary>
public abstract class GameScene : GameObjectContainer, IGameScene
{
    /// <inheritdoc />
    protected override void OnGameObjectAdded(GameObject gameObject)
    {
        base.OnGameObjectAdded(gameObject);

        gameObject.CurrentScene = this;
    }

    public IGameObject FindCollision(GameObject gameObject)
    {
        //return null;
        // TODO: Definite room for improvement.
        return GameObjectsSnapshot.FirstOrDefault(g => !ReferenceEquals(g, gameObject) && g.IsCollisionDetectionEnabled && g.Bounds.IntersectsWith(gameObject.Bounds));
    }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect) => Render(canvas, dirtyRect);
}
