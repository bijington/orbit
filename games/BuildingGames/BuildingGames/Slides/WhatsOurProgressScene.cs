using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class WhatsOurProgressScene : SlideSceneBase
{
	public WhatsOurProgressScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
	{
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("What's our progress?", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"
- Exposed our creative side

- Still within 'typical' bussines application architecture

- Complexity can increase quickly",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.CodeFont,
            (float)Styling.ScaledFontSize(0.05),
            new PointF(40, dimensions.Height * 0.2f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        // TODO: render the Orbit sketch

        base.Render(canvas, dimensions);
    }
}
