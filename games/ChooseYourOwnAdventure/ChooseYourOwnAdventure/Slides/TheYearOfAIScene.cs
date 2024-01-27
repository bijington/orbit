using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TheYearOfAIScene : SlideSceneBase
{
	public TheYearOfAIScene(Pointer pointer) : base(pointer)
    {
	}

    public override string Notes =>
"""
I am sure this is a statement that we are all going to hear lots this year.

I haven't played around too much with AI, the little I have has shown me the power that it can potentially bring to game development. I asked GitHub copilot to provide me with some physics based implementations for the air hockey game.

While the code it provided was not perfect, it was a good starting point and I was able to tweak it to get the results I wanted.

Although it did highlight another statement that we are all likely to reiterate, just throwing AI at a problem doesn't always work.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("2024 - the year of AI", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Storyline creation

- Physics/complicated maths or algorithms

- Visual design
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
