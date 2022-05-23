namespace Orbit.Engine;

/// <summary>
/// Represents a scene or level in a game.
/// </summary>
public abstract class GameScene : IGameScene
{
    private readonly IList<GameObject> gameObjects = new List<GameObject>();

    /// <summary>
    /// Adds the supplied <paramref name="gameObject"/> to the current scene.
    /// This will include it for updates when the game is running.
    /// </summary>
    /// <param name="gameObject">The new <see cref="GameObject"/> to add to the scene.</param>
    public void Add(GameObject gameObject)
    {
        ArgumentNullException.ThrowIfNull(gameObject);

        gameObjects.Add(gameObject);
        gameObject.CurrentScene = this;
    }

    public void Remove(GameObject gameObject)
    {
        ArgumentNullException.ThrowIfNull(gameObject);

        gameObjects.Remove(gameObject);
    }

    public virtual void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var currentObjects = gameObjects.ToList();

        foreach (var gameObject in currentObjects)
        {
            gameObject.Draw(canvas, dirtyRect);
        }
    }

    public GameObject FindCollision(GameObject gameObject)
    {
        // TODO: Definite room for improvement.
        return gameObjects.FirstOrDefault(g => !ReferenceEquals(g, gameObject) && g.IsCollisionDetectionEnabled && g.Bounds.IntersectsWith(gameObject.Bounds));
    }
}
