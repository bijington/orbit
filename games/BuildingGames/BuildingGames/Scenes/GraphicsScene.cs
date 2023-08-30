namespace BuildingGames.Scenes;

public class GraphicsScene : SlideSceneBase
{
    public override bool CanProgress => true;

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
            ".NET MAUI Graphics",
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
            "Unified graphics API",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            "Provides platform specific implementation for us:",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Android - Android.Graphics",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - iOS/MacCatalyst - CoreGraphics",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Windows - Microsoft.Graphics/Windows.Graphics",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);
    }
}
