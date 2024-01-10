using Orbit.Engine;

namespace AirHockey.GameObjects;

public class OpponentPaddle : GameObject
{
    public OpponentPaddle(PlayerStateManager playerStateManager)
    {
        this.playerStateManager = playerStateManager;
    }

    private readonly PlayerStateManager playerStateManager;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var playerState = playerStateManager.OpponentState;

        var x = (float)playerState.X * dimensions.Width;
        var y = (float)Math.Abs(playerState.Y - 1) * dimensions.Height;
        var size = (float)playerState.Size * dimensions.Height;

        canvas.FillColor = Colors.Red;
        canvas.FillCircle(x, y, size);
    }
}
