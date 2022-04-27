namespace Orbit;

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
        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;
        canvas.DrawImage(image, -halfWidth, -halfHeight, image.Width, image.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(-halfWidth, -halfHeight, image.Width, image.Height);
        }
        
        angle += rotationIncrement;
    }
}
