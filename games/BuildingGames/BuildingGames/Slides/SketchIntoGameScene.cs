using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class SketchIntoGameScene : SlideSceneBase
{
	public SketchIntoGameScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Turning sketch into prototype", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"
Demo time!",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.CodeFont,
            (float)Styling.ScaledFontSize(0.05),
            new PointF(40, dimensions.Height * 0.2f),
            HorizontalAlignment.Center,
            VerticalAlignment.Center);

        base.Render(canvas, dimensions);
    }
}
