using System.Reflection;
using Microsoft.Maui.Graphics.Platform;

namespace Orbit;

public class Planet : GameObject
{
    Microsoft.Maui.Graphics.IImage image;

    public Planet()
    {
        var assembly = GetType().GetTypeInfo().Assembly;

        using (var stream = assembly.GetManifestResourceStream("Orbit.Resources.Images.planet.png"))
        {
            image = PlatformImage.FromStream(stream);
        }
    }

    int xy = 0;
    float angle = 0;
    const float rotationIncrement = -0.25f;

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Translate(dirtyRect.Center.X, dirtyRect.Center.Y);
        canvas.Rotate(angle);
        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;
        canvas.DrawImage(image, -halfWidth, -halfHeight, image.Width, image.Height);

        canvas.StrokeColor = Colors.OrangeRed;
        canvas.StrokeSize = 4;
        canvas.DrawEllipse(-halfWidth, -halfHeight, image.Width, image.Height);

        //xy += 10;
        angle += rotationIncrement;
    }
}
