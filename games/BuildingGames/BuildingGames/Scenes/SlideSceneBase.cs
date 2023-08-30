using Orbit.Engine;

namespace BuildingGames.Scenes;

public abstract class SlideSceneBase : GameScene
{
    public abstract bool CanProgress { get; }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var font = Styling.Font;

        canvas.Font = font;
        canvas.FontSize = 25;
        canvas.FontColor = Colors.White;

        canvas.DrawString(
            dimensions,
            "Shaun Lawrence",
            font,
            25,
            new PointF(10, dimensions.Bottom - 40),
            HorizontalAlignment.Left,
            VerticalAlignment.Bottom);

        canvas.DrawString(
            dimensions,
            "@Bijington",
            font,
            25,
            new PointF(-10, dimensions.Bottom - 40),
            HorizontalAlignment.Right,
            VerticalAlignment.Bottom);
    }
}
