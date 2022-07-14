namespace DrawingGame;

public class DrawingManager
{
    private readonly IList<DrawingPath> paths = new List<DrawingPath>();
    private DrawingPath currentPath;

    public bool IsViewing { get; }

    public float LineThickness { get; set; } = 5f;

    public Color SelectedColor { get; set; } = Colors.Black;

    public IReadOnlyList<DrawingPath> Paths => paths.ToList();

    public void StartDrawing(PointF startLocation)
    {
        currentPath = new DrawingPath(SelectedColor, LineThickness);
        currentPath.Add(startLocation);
        paths.Add(currentPath);
    }

    public void UpdateDrawing(PointF currentLocation)
    {
        currentPath.Add(currentLocation);
    }

    public void EndDrawing(PointF endLocation)
    {
        currentPath.Add(endLocation);
    }

    public void Clear()
    {
        paths.Clear();
    }
}
