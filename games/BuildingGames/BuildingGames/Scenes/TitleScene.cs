namespace BuildingGames.Scenes;

public class TitleScene : SlideSceneBase
{
    private float startAlpha = 1f;
    private float increment = -0.05f;

    public TitleScene()
    {
    }

    public override bool CanProgress => true;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var font = Styling.Font;

        canvas.Font = font;
        canvas.FontSize = 80;
        canvas.FontColor = Colors.White;
        canvas.SetShadow(new SizeF(5, 5), 5, Colors.Black);

        canvas.DrawString(
            new RectF(40, 0, dimensions.Width - 80, dimensions.Height),
            "Building games in .NET MAUI",
            font,
            80,
            new PointF(40, dimensions.Height * 0.25f),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        canvas.Alpha = startAlpha;
        canvas.FontSize = 25;
        canvas.FontColor = Colors.Yellow;

        canvas.DrawString(
            dimensions,
            "PRESS START",
            font,
            25,
            new PointF(0, dimensions.Height * 0.75f),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        startAlpha = Math.Clamp(startAlpha + increment, 0f, 1f);

        if (startAlpha == 0f)
        {
            increment = 0.05f;
        }
        else if (startAlpha == 1f)
        {
            increment = -0.05f;
        }
    }
}
