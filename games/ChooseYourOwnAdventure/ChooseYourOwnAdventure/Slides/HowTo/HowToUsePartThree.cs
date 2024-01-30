using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class HowToUsePartThree : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly float aspectRatio;

    public HowToUsePartThree(Pointer pointer) : base(pointer)
    {
        image = LoadImage("how_to_part_three.png");
        aspectRatio = image.Width / image.Height;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("How to use - GameScene", canvas, dimensions);

        var imageHeight = dimensions.Height * 0.7f;
        var imageWidth = imageHeight * aspectRatio;

        canvas.DrawImage(
            image,
            dimensions.Center.X - imageWidth / 2,
            dimensions.Height - imageHeight * 1.2f,
            imageWidth,
            imageHeight);

        base.Render(canvas, dimensions);

        _ = 
"""
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

""";
    }
}
