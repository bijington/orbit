using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TheFinalBossScene : SlideSceneBase
{
	public TheFinalBossScene(Pointer pointer) : base(pointer)
	{
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("The final boss", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Better Accessibility options

- Physics - Collisions / Gravity

- Performance benchmarking

- .NET Hot Reload support

- Testing support",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }
}
