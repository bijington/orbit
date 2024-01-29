using AirHockey.GameObjects;
using Orbit.Engine;

namespace AirHockey.Scenes;

public class MainScene : GameScene
{
    public MainScene(
        Paddle playerPaddle,
        OpponentPaddle opponentPaddle,
        Puck puck,
        ScoreDisplay playerScore,
        ScoreDisplay opponentScore,
        GameManager gameManager)
    {
        Add(playerPaddle);
        Add(opponentPaddle);
        Add(gameManager);

        playerScore.ScoreIndex = 0;
        playerScore.PlayerColor = Colors.Blue;
        opponentScore.ScoreIndex = 1;
        opponentScore.PlayerColor = Colors.Red;

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
}
