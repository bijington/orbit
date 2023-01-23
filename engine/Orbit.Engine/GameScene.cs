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
        // TODO: Definite room for improvement.
        return GameObjectsSnapshot.FirstOrDefault(g => !ReferenceEquals(g, gameObject) && g.IsCollisionDetectionEnabled && IsIntersection(g.Bounds, gameObject.Bounds));//g.Bounds.IntersectsWith(gameObject.Bounds));
    }

    // TODO: This clearly only works for a circle, need to find a way to define custom shapes.
    private bool IsIntersection(RectF one, RectF two)
    {
        double x1 = one.Center.X;
        double x2 = two.Center.X;
        double y1 = one.Center.Y;
        double y2 = two.Center.Y;

        double r1 = one.Width / 2;
        double r2 = two.Width / 2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        return d <= Math.Abs(r1 - r2) || d <= r1 + r2;
    }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect) => Render(canvas, dirtyRect);
}
