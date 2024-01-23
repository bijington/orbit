using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class DuckTypingScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public DuckTypingScene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("duck_typing.png");
	}

    public override string Notes => 
"""
I'll be honest this might seem like a bit of a stretch but bear with me. I need to get in as many game references as I can!

If you are not familiar with the term duck typing, it is a term used in programming to determine whether an object can be used for a particular purpose. This, I believe is a perfect analogy for building UIs which are a highly important part of a game.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Help your users", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- If it looks like a duck, and quacks like a duck, it's a duck

    - If you need your user to click/tap then use a button

- If you can't find a duck, teach what you can to quack

    - If you can't use a button then make sure your controls are accessible
""", // TODO: Add a slide with a duck
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Right - imageWidth - 80, dimensions.Bottom - imageHeight - 80, imageWidth, imageHeight);

        base.Render(canvas, dimensions);
    }
}
