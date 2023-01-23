using Orbit.Engine;

namespace Orbit.GameObjects;

public class Shadow : GameObject
{
    public Planet Planet { get; set; }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Translate(dimensions.Center.X, dimensions.Center.Y);

        PathF path = new PathF();

        path.MoveTo(Planet.Bounds.Width / 3, -(Planet.Bounds.Height / 3));
        path.LineTo(dimensions.Width, dimensions.Height / 4);
        path.LineTo(dimensions.Width / 4, dimensions.Height);
        path.LineTo(-(Planet.Bounds.Width / 3), Planet.Bounds.Height / 3);

        canvas.FillColor = Colors.Black;
        canvas.Alpha = 0.6f;
        canvas.FillPath(path);
    }
}
