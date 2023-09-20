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

    public RectF Bounds { get; protected set; }

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

    public virtual bool IsCollisionDetectionEnabled { get; }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();
        //canvas.ResetState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }

    protected override void OnGameObjectAdded(GameObject gameObject)
    {
        base.OnGameObjectAdded(gameObject);

        gameObject.CurrentScene = this.CurrentScene;
    }

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
