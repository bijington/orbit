using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class WhatsOurProgressScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage sketch;
    private readonly float aspectRatio;

	public WhatsOurProgressScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
	{
        sketch = LoadImage("orbit_sketch.jpg");
        aspectRatio = sketch.Width / sketch.Height;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("What's our progress?", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Exposed our creative side

- Still within 'typical' business application architecture

- Complexity can increase quickly",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var imageHeight = dimensions.Height * 0.4f;
        var imageWidth = imageHeight * aspectRatio;

        canvas.DrawImage(
            sketch,
            dimensions.Center.X - imageWidth / 2,
            dimensions.Height - imageHeight * 1.2f,
            imageWidth,
            imageHeight);

        base.Render(canvas, dimensions);
    }
}
