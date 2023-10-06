using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TheBirthOfAnotherDistractionScene : SlideSceneBase
{
    public TheBirthOfAnotherDistractionScene(Pointer pointer) : base(pointer)
    {
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("The birth of another distraction", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- What if I want to build more than one game?

- What can I reuse?

- Let's disappear down this rabbit hole

- I wonder if there is cake down there too...",
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
