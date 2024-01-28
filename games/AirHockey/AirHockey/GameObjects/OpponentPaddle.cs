using System.Net.Http.Headers;
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

        if (playerState is null)
        {
            return;
        }
        
        var x = (float)playerState.X * dimensions.Width;
        var y = (float)Math.Abs(playerState.Y - 1) * dimensions.Height;
        var size = (float)playerState.Size * dimensions.Width;

        var radius = size / 2;

        canvas.FillColor = playerState.IsBottom ? Colors.Red : Colors.Blue;
        canvas.FillCircle(x, y, radius);
    }
}
