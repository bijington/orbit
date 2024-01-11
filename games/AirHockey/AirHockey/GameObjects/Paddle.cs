using Orbit.Engine;

namespace AirHockey.GameObjects;

public class Paddle : GameObject
{
    public Paddle(PlayerStateManager playerStateManager)
    {
        this.playerStateManager = playerStateManager;
    }

    private readonly PlayerStateManager playerStateManager;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var playerState = playerStateManager.PlayerState;

        var x = (float)playerState.X * dimensions.Width;
        var y = (float)playerState.Y * dimensions.Height;
        var size = (float)playerState.Size * dimensions.Width;

        canvas.FillColor = playerState.IsBottom ? Colors.Red : Colors.Blue;
        canvas.FillCircle(x, y, size);
    }
}
