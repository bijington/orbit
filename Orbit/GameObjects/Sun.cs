using Orbit.Engine;

namespace Orbit.GameObjects;

public class Sun : GameObject
{
    Microsoft.Maui.Graphics.IImage image;

    public Sun()
    {
        image = LoadImage("sun.png");
    }

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;
        canvas.DrawImage(image, -halfWidth, -halfHeight, image.Width, image.Height);
    }
}
