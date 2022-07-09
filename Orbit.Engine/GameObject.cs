using System.Reflection;
using Microsoft.Maui.Graphics.Platform;

namespace Orbit.Engine;

/// <summary>
/// Base class definition representing an object in a game.
/// </summary>
public abstract class GameObject : IGameObject, IDrawable
{
    public RectF Bounds { get; protected set; }

    public GameScene CurrentScene { get; internal set; } // TODO: weak reference?

    public virtual bool IsCollisionDetectionEnabled { get; }

    protected Microsoft.Maui.Graphics.IImage LoadImage(string imageName)
    {
        var assembly = GetType().GetTypeInfo().Assembly;

        using (var stream = assembly.GetManifestResourceStream("Orbit.Resources.EmbeddedResources." + imageName))
        {
            return PlatformImage.FromStream(stream);
        }
    }

    /// <inheritdoc />
    public virtual void Render(ICanvas canvas, RectF dimensions)
    {
    }

    /// <inheritdoc />
    public virtual void Update(double millisecondsSinceLastUpdate)
    {
    }

    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();
        canvas.ResetState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }
}
