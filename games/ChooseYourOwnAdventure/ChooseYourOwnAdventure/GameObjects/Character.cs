using Orbit.Engine;

namespace ChooseYourOwnAdventure.GameObjects;

public class Character : GameObject
{
    private readonly Microsoft.Maui.Graphics.IImage image;
    private static PointF position;

    public Character()
    {
        image = LoadImage("character.png");

        Position = Positions.Starting;
    }

    public static PointF Position
    {
        get => position;
        set
        {
            position = value;
            Journey.Add(value);
        }
    }

    public SizeF TileSize { get; set; }

    public static IList<PointF> Journey { get; set; } = new List<PointF>();

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.DrawImage(
            image,
            Position.X * TileSize.Width,
            Position.Y * TileSize.Height,
            TileSize.Width,
            TileSize.Height);
    }

    internal static class Positions
    {
        public static PointF Starting => new(4, 4);
        public static PointF Tutorial1 => new(6, 4);
        public static PointF Tutorial2 => new(8, 4);
        public static PointF Tutorial3 => new(10, 4);
        public static PointF Tutorial4 => new(12, 4);
        public static PointF Decision1 => new(14, 4);
        public static PointF Sports1 => new(14, 7);
        public static PointF Sports2 => new(15, 7);
        public static PointF Sports3 => new(15, 9);
        public static PointF Sports4 => new(14, 9);
        public static PointF Sports5 => new(14, 12);
        public static PointF Democracy1 => new(22, 4);
        public static PointF Democracy2 => new(22, 8);
        public static PointF Democracy3 => new(22, 12);
        public static PointF Decision2 => new(10, 12);
        public static PointF Accessibility1 => new(10, 9);
        public static PointF Accessibility2 => new(6, 9);
        public static PointF Accessibility3 => new(6, 15);
        public static PointF Skip1 => new(10, 15);
        public static PointF AppStore => new(3, 15);
    }
}
