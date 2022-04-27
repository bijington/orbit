namespace Orbit;

public class Asteroid : GameObject
{
	Microsoft.Maui.Graphics.IImage image;
    int speed = 1;
    PointF origin;
    PointF destination;
    int x;
    int y;

	public Asteroid()
	{
		image = LoadImage("asteroid.png");

        // Randomly generate locations + speed.
        origin = new PointF(0, 0);
        destination = new PointF(200, 200);
	}

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        // TODO: Actually calc the correct path.
        x += speed;
        y += speed;

        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;
        canvas.DrawImage(image, x + -halfWidth, y + -halfHeight, image.Width, image.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(x + -halfWidth, y + -halfHeight, image.Width, image.Height);
        }
    }
}
