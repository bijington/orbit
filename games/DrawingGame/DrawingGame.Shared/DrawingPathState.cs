using System.Drawing;

namespace DrawingGame.Shared;

public class DrawingPathState
{
	public string Color { get; set; } = "Black";

	public int Thickness { get; set; } = 5;

	public IList<Point> Points { get; set; } = new List<Point>();
}

