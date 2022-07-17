namespace DrawingGame;

public class DrawingPath
{
    public DrawingPath(short colorIndex, float thickness)
    {
        ColorIndex = colorIndex;
        Thickness = thickness;

        Path = new PathF();
    }

    public short ColorIndex { get; }
    public PathF Path { get; }
    public float Thickness { get; }

    public void Add(PointF point) => Path.LineTo(point);
}
