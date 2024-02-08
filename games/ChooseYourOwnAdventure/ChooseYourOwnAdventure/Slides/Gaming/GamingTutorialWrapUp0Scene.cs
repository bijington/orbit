using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorialWrapUp0Scene : SlideSceneBase
{
	public GamingTutorialWrapUp0Scene(Pointer pointer) : base(pointer)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("What did we build?", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Server side management of game state

- Thin client

- Code sharing to make development easier
""",
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
