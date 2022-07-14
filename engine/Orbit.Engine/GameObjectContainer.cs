namespace Orbit.Engine;

/// <summary>
/// Base class implementation for containing <see cref="GameObject"/> children.
/// </summary>
public abstract class GameObjectContainer : IGameObjectContainer, IRender, IUpdate
{
    private readonly IList<IGameObject> gameObjects = new List<IGameObject>();

    protected IList<IGameObject> GameObjectsSnapshot => gameObjects.ToList();

    /// <inheritdoc />
    public void Add(GameObject gameObject)
    {
        ArgumentNullException.ThrowIfNull(gameObject);

        gameObjects.Add(gameObject);

        OnGameObjectAdded(gameObject);
    }

    /// <inheritdoc />
    public void Remove(GameObject gameObject)
    {
        ArgumentNullException.ThrowIfNull(gameObject);

        gameObjects.Remove(gameObject);

        OnGameObjectRemoved(gameObject);
    }

    protected virtual void OnGameObjectAdded(GameObject gameObject)
    {

    }

    protected virtual void OnGameObjectRemoved(GameObject gameObject)
    {

    }

    /// <inheritdoc />
    public virtual void Render(ICanvas canvas, RectF dimensions)
    {
        var currentObjects = GameObjectsSnapshot;

        foreach (var gameObject in currentObjects)
        {
            ((IDrawable)gameObject).Draw(canvas, dimensions);
        }
    }

    /// <inheritdoc />
    public virtual void Update(double millisecondsSinceLastUpdate)
    {
        var currentObjects = GameObjectsSnapshot;

        foreach (var gameObject in currentObjects)
        {
            gameObject.Update(millisecondsSinceLastUpdate);
        }
    }
}
