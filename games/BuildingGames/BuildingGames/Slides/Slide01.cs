namespace BuildingGames.Slides;

public class Slide01 : SlideSceneBase
{
    private float startAlpha = 1f;
    private float increment = -0.05f;

    public Slide01()
    {
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        // TODO this isn't quite right, maybe pass in a different set of dimensions
        Styling.RenderTitle("Building games in .NET MAUI", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            "PRESS START",
            Colors.Yellow,
            Colors.Transparent,
            startAlpha,
            Styling.Font,
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
