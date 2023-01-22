using Orbit.Engine;

namespace Orbit.GameObjects;

public class PlanetHealthIndicator : GameObject
{
    private readonly Planet planet;

    public PlanetHealthIndicator(
		Planet planet)
	{
        this.planet = planet;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        float height = dimensions.Height / 2;
        float initialY = height / 2;

        canvas.Translate(dimensions.Width - 60, dimensions.Center.Y);
        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.Gray;
        canvas.DrawLine(30, -initialY, 30, initialY);

        float actualLevelHeight = height * (this.planet.HealthPoints / this.planet.MaxHealthPoints);
        float y = height - actualLevelHeight;

        canvas.StrokeSize = 20;
        canvas.StrokeColor = Colors.DarkRed;
        canvas.DrawLine(30, y - initialY, 30, initialY);
    }
}
