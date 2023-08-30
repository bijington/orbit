using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class DistanceCounter : GameObject
{
    private readonly StateObject stateObject;

    public DistanceCounter(StateObject stateObject)
	{
        this.stateObject = stateObject;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.FontSize = 20;
        canvas.FontColor = Colors.White;
        canvas.SetShadow(new SizeF(5, 5), 5, Colors.Black);

        canvas.DrawString(
            dimensions,
            $"Distance: {stateObject.DistanceTraveled}m",
            Microsoft.Maui.Graphics.Font.Default,
            20,
            new PointF(0, dimensions.Height * 0.25f),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);
    }
}
