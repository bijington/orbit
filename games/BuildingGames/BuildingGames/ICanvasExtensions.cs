namespace BuildingGames;

public static class ICanvasExtensions
{
    public static void DrawString(
        this ICanvas canvas,
        RectF dimensions,
        string text,
        Color textColor,
        Color shadowColor,
        float alpha,
        IFont font,
        float fontSize,
        PointF location,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment)
    {
        canvas.Alpha = alpha;
        canvas.Font = font;
        canvas.FontSize = fontSize;
        canvas.FontColor = textColor;

        canvas.SetShadow(new SizeF(0, 0), 0, shadowColor);

        var size = canvas.GetStringSize(text, font, fontSize, horizontalAlignment, verticalAlignment);

        var lineCount = MathF.Ceiling(size.Width / dimensions.Width) + 1;

        //canvas.StrokeColor = Colors.Orange;
        //canvas.DrawRectangle(new RectF(location, new SizeF(dimensions.Width, size.Height * lineCount)));

        canvas.DrawString(
            text,
            new RectF(location, new SizeF(dimensions.Width, size.Height * lineCount)),
            horizontalAlignment,
            verticalAlignment,
            TextFlow.OverflowBounds);
    }
}

