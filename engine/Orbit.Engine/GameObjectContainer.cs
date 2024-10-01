using System.Diagnostics;
using System.Reflection;

namespace Orbit.Engine;

/// <summary>
/// Base class implementation for containing <see cref="GameObject"/> children.
/// </summary>
public abstract class GameObjectContainer : IGameObjectContainer, IRender, IUpdate
{
    private readonly IList<IGameObject> gameObjects = new List<IGameObject>();

    /// <summary>
    /// Gets a snapshot of the game objects in the container.
    /// </summary>
    public IReadOnlyList<IGameObject> GameObjectsSnapshot => gameObjects.ToList();

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

    /// <summary>
    /// Lifecycle method called to inform a container that a <see cref="GameObject"/> has been added as a child of the container.
    /// </summary>
    /// <param name="gameObject">The <see cref="GameObject"/> that has been added to the container.</param>
    /// <remarks>
    /// Use this to perform any initialization that may be required when a new <see cref="GameObject"/> is added.
    /// </remarks>
    protected virtual void OnGameObjectAdded(GameObject gameObject)
    {
        gameObject.OnAdded();
    }

    /// <summary>
    /// Lifecycle method called to inform a container that a <see cref="GameObject"/> has been removed as a child of the container.
    /// </summary>
    /// <param name="gameObject">The <see cref="GameObject"/> that has been removed from the container.</param>
    /// <remarks>
    /// Use this to tidy up any resource that may be required when a <see cref="GameObject"/> is removed from the container and most likely the game.
    /// </remarks>
    protected virtual void OnGameObjectRemoved(GameObject gameObject)
    {
        gameObject.OnRemoved();
    }

    /// <summary>
    /// Loads an image from the specified resource name.
    /// </summary>
    /// <param name="imageName">The name of the image resource.</param>
    /// <returns>The loaded image.</returns>
    protected Microsoft.Maui.Graphics.IImage LoadImage(string imageName)
    {
        var assembly = GetType().GetTypeInfo().Assembly;

        using var stream = assembly.GetManifestResourceStream(imageName);

#if WINDOWS
        return new Microsoft.Maui.Graphics.Win2D.W2DImageLoadingService().FromStream(stream);
#else
        return Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
#endif
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
