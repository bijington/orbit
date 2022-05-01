using Orbit.Engine;

namespace Orbit.GameObjects;

public class Shadow : GameObject
{
    private readonly Planet planet;

    public Shadow(Planet planet)
	{
        this.planet = planet;
    }

    public override void Render(ICanvas canvas, RectF dirtyRect)
    {
        base.Render(canvas, dirtyRect);

        canvas.Translate(dirtyRect.Center.X, dirtyRect.Center.Y);

        PathF path = new PathF();
        path.MoveTo(planet.Bounds.Width / 3, 0);
        path.LineTo(dirtyRect.Width, dirtyRect.Height / 4);
        path.LineTo(dirtyRect.Width / 4, dirtyRect.Height);
        path.LineTo(0, planet.Bounds.Height / 3);
        canvas.FillColor = Colors.Black;
        canvas.Alpha = 0.6f;
        canvas.FillPath(path);
    }
}
