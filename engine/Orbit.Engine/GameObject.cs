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
    public RectF Bounds { get; protected set; }

    public GameScene CurrentScene { get; internal set; } // TODO: weak reference?

    public virtual bool IsCollisionDetectionEnabled { get; }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();
        canvas.ResetState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }
}
