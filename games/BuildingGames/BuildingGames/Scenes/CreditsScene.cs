namespace BuildingGames.Scenes;

public class CreditsScene : SlideSceneBase
{
    public override bool CanProgress => false;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var font = Styling.Font;

        canvas.Font = font;
        canvas.FontSize = 80;
        canvas.FontColor = Colors.White;
        canvas.SetShadow(new SizeF(5, 5), 5, Colors.Black);

        float y = dimensions.Height * 0.15f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            "Credits",
            font,
            80,
            new PointF(40, y),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        y += 200f;

        canvas.Font = font;
        canvas.FontSize = 40;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            "https://github.com/bijington/orbit",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        y += 200f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            "https://www.telerik.com/paddle-boarding-in-maui",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);
    }
}
