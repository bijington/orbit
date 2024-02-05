using System.Reflection;
using Microsoft.Maui.Graphics;
#if WINDOWS
using Microsoft.Maui.Graphics.Win2D;
#else
using Microsoft.Maui.Graphics.Platform;
#endif

namespace Orbit.Engine;

/// <summary>
/// Base class definition representing an object in a game.
/// </summary>
public abstract class GameObject : GameObjectContainer, IGameObject, IDrawable
{
    private GameScene currentScene;

    /// <inheritdoc />
    public RectF Bounds { get; protected set; }

    /// <inheritdoc />
    public GameScene CurrentScene
    {
        get => this.currentScene;
        set
        {
            if (this.currentScene != value)
            {
                this.currentScene = value;

                this.UpdateCurrentScene();
            }
        }
    }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }

    /// <summary>
    /// Lifecycle method called to inform this <see cref="GameObject"/> that it has been added to a container and ultimately a game.
    /// </summary>
    /// <remarks>
    /// Use this to perform any initialization that may be required when this <see cref="GameObject"/> is added.
    /// </remarks>
    public virtual void OnAdded()
    {
    }

    /// <summary>
    /// Lifecycle method called to inform this <see cref="GameObject"/> that it has been removed from a container and ultimately a game.
    /// </summary>
    /// <remarks>
    /// Use this to tidy up any resource that may be required when this <see cref="GameObject"/> is removed from a container and most likely a game.
    /// </remarks>
    public virtual void OnRemoved()
    {
    }

    /// <inheritdoc />
    protected override void OnGameObjectAdded(GameObject gameObject)
    {
        base.OnGameObjectAdded(gameObject);

        gameObject.CurrentScene = this.CurrentScene;
    }

    /// <inheritdoc />
    protected override void OnGameObjectRemoved(GameObject gameObject)
    {
        base.OnGameObjectRemoved(gameObject);

        gameObject.CurrentScene = null;
    }

    private void UpdateCurrentScene()
    {
        foreach (var gameObject in GameObjectsSnapshot)
        {
            gameObject.CurrentScene = this.CurrentScene; 
        }
    }
}
