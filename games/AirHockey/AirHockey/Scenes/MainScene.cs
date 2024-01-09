using AirHockey.GameObjects;
using Orbit.Engine;

namespace AirHockey.Scenes;

public class MainScene : GameScene
{
    private readonly Paddle playerPaddle;
    private readonly Paddle opponentPaddle;

    public MainScene(
        Paddle playerPaddle,
        Paddle opponentPaddle,
        Puck puck,
        ScoreDisplay playerScore,
        ScoreDisplay opponentScore)
    {
        playerPaddle.Color = Colors.Orange;
        Add(playerPaddle);
        this.playerPaddle = playerPaddle;

        opponentPaddle.Color = Colors.Blue;
        Add(opponentPaddle);
        this.opponentPaddle = opponentPaddle;

        playerScore.ScoreIndex = 0;
        opponentScore.ScoreIndex = 1;

        Add(puck);
        Add(playerScore);
        Add(opponentScore);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        canvas.StrokeSize = 10;
        canvas.StrokeColor = Colors.Red;

        canvas.DrawLine(0, dimensions.Center.Y, dimensions.Width, dimensions.Center.Y);
        canvas.DrawCircle(dimensions.Center, 50);
        canvas.FillColor = Colors.White;
        canvas.FillCircle(dimensions.Center, 45);
        canvas.FillColor = Colors.Red;
        canvas.FillCircle(dimensions.Center, 5);

        base.Render(canvas, dimensions);
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
