using Orbit.Engine;

namespace Orbit.GameObjects;

public class Planet : GameObject
{
    Microsoft.Maui.Graphics.IImage image;
    float angle = 0;
    const float rotationIncrement = -0.25f;

    public Planet()
    {
        image = LoadImage("planet.png");
    }

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Translate(dirtyRect.Center.X, dirtyRect.Center.Y);
        canvas.Rotate(angle);
        var halfWidth = dirtyRect.Width / 4;//image.Width / 2;
        var halfHeight = dirtyRect.Height / 4;//image.Height / 2;

        Bounds = new RectF(-halfHeight, -halfHeight, halfWidth * 2, halfHeight * 2);

        canvas.DrawImage(image, Bounds.X, Bounds.Y, Bounds.Height, Bounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(Bounds.X, Bounds.Y, Bounds.Height, Bounds.Height);
        }

        angle += rotationIncrement;
    }
}
