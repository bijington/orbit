using System.Reflection;
using Microsoft.Maui.Graphics.Platform;

namespace Orbit.Engine;

public abstract class GameObject : IGameObject
{
    public RectF Bounds { get; protected set; }

    public bool IsCollisionDetectionEnabled { get; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();
        canvas.ResetState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }

    protected Microsoft.Maui.Graphics.IImage LoadImage(string imageName)
    {
        var assembly = GetType().GetTypeInfo().Assembly;

        using (var stream = assembly.GetManifestResourceStream("Orbit.Resources.Images." + imageName))
        {
            return PlatformImage.FromStream(stream);
        }
    }

    public virtual void Render(ICanvas canvas, RectF dirtyRect)
    {
        if (IsCollisionDetectionEnabled)
        {

        }
    }
}
