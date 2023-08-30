namespace BuildingGames;

public static class ICanvasExtensions
{
    public static void DrawString(
        this ICanvas canvas,
        RectF dimensions,
        string text,
        IFont font,
        float fontSize,
        PointF location,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment)
    {
        var size = canvas.GetStringSize(text, font, fontSize, horizontalAlignment, verticalAlignment);

        var lineCount = MathF.Ceiling(size.Width / dimensions.Width);

        canvas.DrawString(
            text,
            new RectF(location, new SizeF(dimensions.Width, size.Height * lineCount)),
            horizontalAlignment,
            verticalAlignment);
    }
}

