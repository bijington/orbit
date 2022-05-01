using Orbit.Engine;

namespace Orbit.GameObjects;

public class Asteroid : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    Microsoft.Maui.Graphics.IImage image;
    int speed = 1;
    PointF origin;
    PointF destination;
    int x;
    int y;

	public Asteroid(IGameSceneManager gameSceneManager)
	{
		image = LoadImage("asteroid.png");


        // Randomly generate locations + speed.
        origin = new PointF(0, 0);
        destination = new PointF(200, 200);
        this.gameSceneManager = gameSceneManager;
    }

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        // TODO: Actually calc the correct path.
        x += speed;
        y += speed;

        var halfWidth = image.Width / 2;
        var halfHeight = image.Height / 2;

        Bounds = new RectF(x + -halfWidth, y + -halfHeight, image.Width, image.Height);

        canvas.DrawImage(image, x + -halfWidth, y + -halfHeight, image.Width, image.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(x + -halfWidth, y + -halfHeight, image.Width, image.Height);
        }

        var collision = gameSceneManager.FindCollision(this);

        if (collision is not null)
        {
            var i = 0;
        }
    }
}
