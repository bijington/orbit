using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class Log : GameObject
{
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly StateObject stateObject;
    private float logX = float.NaN;

    public Log(StateObject stateObject)
	{
        image = LoadImage("log.png");
        this.stateObject = stateObject;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        if (float.IsNaN(logX))
        {
            logX = dimensions.Width;
        }

        canvas.DrawImage(
            image,
            logX,
            dimensions.Center.Y,
            image.Width,
            image.Height);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (!float.IsNaN(logX))
        {
            var change = stateObject.CurrentFlowRate / (float)millisecondsSinceLastUpdate;
            logX -= change;
        }
    }
}

