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
            TextFlow.ClipBounds);
    }

    public static void DrawCenteredScaledImage(
        this ICanvas canvas,
        Microsoft.Maui.Graphics.IImage image,
        RectF dimensions,
        float scale)
    {
        var aspectRatio = image.Width / image.Height;

        var height = dimensions.Height * scale;
        var width = height * aspectRatio;
        
        if (width > dimensions.Width)
        {
            width = dimensions.Width * scale;
            height = width / aspectRatio;
        }

        canvas.DrawImage(image, dimensions.Center.X - width / 2, dimensions.Center.Y - height / 2, width, height);
    }
}

