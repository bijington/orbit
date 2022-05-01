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
    }

    public virtual void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.Draw(canvas, dirtyRect);
        }
    }

    public GameObject FindCollision(GameObject gameObject)
    {
        var a = gameObjects.Where(g => !ReferenceEquals(g, gameObject) && g.IsCollisionDetectionEnabled && g.Bounds.IntersectsWith(gameObject.Bounds)).Select(g => g.Bounds).ToList();

        Console.WriteLine($"BOUNDS = {gameObject.Bounds}");
        Console.WriteLine($"COLLISION = {string.Join(';', a)}");

        var b = gameObjects.Where(g => !ReferenceEquals(g, gameObject)).Select(g => g.Bounds).ToArray();

        //Console.WriteLine($"COLLISION = {string.Join(';', b)}");

        // TODO: Need to handle the actual origin (0,0) as most are currently using the centre of the screen.

        return gameObjects.FirstOrDefault(g => !ReferenceEquals(g, gameObject) && g.Bounds.IntersectsWith(gameObject.Bounds));
    }
}
