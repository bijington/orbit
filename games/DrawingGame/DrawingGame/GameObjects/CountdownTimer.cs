using Orbit.Engine;
using Font = Microsoft.Maui.Graphics.Font;

namespace DrawingGame.GameObjects;

public class CountdownTimer : GameObject
{
    private readonly DrawingManager drawingManager;
    private TimeSpan timeSpan = TimeSpan.FromSeconds(80);

    public CountdownTimer(DrawingManager drawingManager)
    {
        this.drawingManager = drawingManager;

        drawingManager.TimeRemaining = timeSpan;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (millisecondsSinceLastUpdate > 20)
        {
            return;
        }

        drawingManager.TimeRemaining = drawingManager.TimeRemaining.Subtract(TimeSpan.FromMilliseconds(millisecondsSinceLastUpdate));
    }
}
