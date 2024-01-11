using Orbit.Engine;

namespace AirHockey.GameObjects;

public class Puck : GameObject
{
    public Puck(PlayerStateManager playerStateManager)
    {
        this.playerStateManager = playerStateManager;
    }

    private readonly PlayerStateManager playerStateManager;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var puckState = playerStateManager.PuckState;

        var x = (float)puckState.X * dimensions.Width;
        var y = (float)puckState.Y * dimensions.Height;

        if (this.playerStateManager.PlayerState.IsBottom is false)
        {
            y = (float)Math.Abs(puckState.Y - 1) * dimensions.Height;
        }

        var size = (float)puckState.Size * dimensions.Width;

        canvas.FillColor = Colors.Orange;
        canvas.FillCircle(x, y, size);
    }
}
