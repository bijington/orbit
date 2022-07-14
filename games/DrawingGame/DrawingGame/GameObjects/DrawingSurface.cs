using Orbit.Engine;

namespace DrawingGame.GameObjects;

public class DrawingSurface : GameObject
{
    private readonly DrawingManager drawingManager;

    public DrawingSurface(DrawingManager drawingManager)
    {
        this.drawingManager = drawingManager;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        foreach (var path in drawingManager.Paths)
        {
            DrawPath(path, canvas);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }

    private void DrawPath(DrawingPath path, ICanvas canvas)
    {
        canvas.StrokeColor = path.Color;
        canvas.StrokeSize = path.Thickness;
        canvas.StrokeLineCap = LineCap.Round;
        canvas.DrawPath(path.Path);
    }
}
