using AirHockey.GameObjects;
using Orbit.Engine;

namespace AirHockey.Scenes;

public class MainScene : GameScene
{
    private readonly Paddle playerPaddle;
    private readonly Paddle opponentPaddle;

    public MainScene(
        Paddle playerPaddle,
        Paddle opponentPaddle)
    {
        playerPaddle.Color = Colors.Orange;
        Add(playerPaddle);
        this.playerPaddle = playerPaddle;

        opponentPaddle.Color = Colors.Blue;
        Add(opponentPaddle);
        this.opponentPaddle = opponentPaddle;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.StrokeSize = 10;
        canvas.StrokeColor = Colors.Red;

        canvas.DrawLine(0, dimensions.Center.Y, dimensions.Width, dimensions.Center.Y);
        canvas.DrawCircle(dimensions.Center, 50);
        canvas.FillColor = Colors.White;
        canvas.FillCircle(dimensions.Center, 45);
        canvas.FillColor = Colors.Red;
        canvas.FillCircle(dimensions.Center, 5);
    }

    public void UpdatePlayerState(float x, float y)
    {
        playerPaddle.UpdatePlayerState(x, y);
    }

    public void UpdateOpponentPlayerState(float x, float y)
    {
        opponentPaddle.UpdatePlayerState(x, y);
    }
}
