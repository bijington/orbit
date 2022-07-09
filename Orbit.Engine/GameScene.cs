namespace Orbit.Engine;

/// <summary>
/// Base class definition representing a scene or level in a game.
/// </summary>
public abstract class GameScene : IGameScene
{
    private readonly IList<IGameObject> gameObjects = new List<IGameObject>();

    /// <inheritdoc />
    public void Add(GameObject gameObject)
    {
        ArgumentNullException.ThrowIfNull(gameObject);

        gameObjects.Add(gameObject);
        gameObject.CurrentScene = this;
    }

    /// <inheritdoc />
    public void Remove(GameObject gameObject)
    {
        ArgumentNullException.ThrowIfNull(gameObject);

        gameObjects.Remove(gameObject);
    }

    /// <inheritdoc />
    public virtual void Render(ICanvas canvas, RectF dimensions)
    {
        var currentObjects = gameObjects.ToList();

        foreach (var gameObject in currentObjects)
        {
            ((IDrawable)gameObject).Draw(canvas, dimensions);
        }
    }

    /// <inheritdoc />
    public virtual void Update(double millisecondsSinceLastUpdate)
    {
        var currentObjects = gameObjects.ToList();

        foreach (var gameObject in currentObjects)
        {
            gameObject.Update(millisecondsSinceLastUpdate);
        }
    }

    public GameObject FindCollision(GameObject gameObject)
    {
        return null;
        // TODO: Definite room for improvement.
        //return gameObjects.FirstOrDefault(g => !ReferenceEquals(g, gameObject) && g.IsCollisionDetectionEnabled && g.Bounds.IntersectsWith(gameObject.Bounds));
    }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect) => Render(canvas, dirtyRect);
}
