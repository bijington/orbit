using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class DuckTypingScene : SlideSceneBase
{
    private int currentTransition = 0;
    private readonly int transitions = 2;

    private readonly Microsoft.Maui.Graphics.IImage[] images;
    private readonly string[] textTransitions;

	public DuckTypingScene(Pointer pointer) : base(pointer)
    {
        images = [LoadImage("duck_typing_2.png"), LoadImage("duck_typing_3.png"), LoadImage("duck_typing_1.png")];
        transitions = images.Length;

        textTransitions = [
            """
- If it looks like a duck, and quacks like a duck, it's a duck

    - If you need your user to click/tap then use a button
""",
"""
- If it looks like a duck, and quacks like a duck, it's a duck

    - If you need your user to click/tap then use a button

- If you can't find a duck, teach what you can find to quack

    - If you can't use a button then make sure your controls are accessible
""",
"""
- If it looks like a duck, and quacks like a duck, it's a duck

    - If you need your user to click/tap then use a button

- If you can't find a duck, teach what you can find to quack

    - If you can't use a button then make sure your controls are accessible

- Let's help this guy weed out the ducks
"""
        ];
	}

    public override void Progress()
    {
        // If we are complete then fire the Next event.
        if (currentTransition == transitions)
        {
            base.Progress();
        }

        currentTransition++;
    }

    public override string Notes => 
"""
If you are not familiar with the term duck typing, it is a term used in programming to determine whether an object can be used for a particular purpose. This, I believe is a perfect analogy for building UIs which are a highly important part of a game.

If you want to present something to a user that can be clicked we should use a button.

Why I hear you ask? Well for users that have visual impairments they will be able to use a screen reader to navigate the UI and the screen reader will be able to tell them that the control is a button and that it can be clicked.

If you can't make use of a button for whatever reason you find (perhaps it won't style quite right) then make whatever you use look and quack like a button. To help the screen reader and ultimately help your users.

I'll be honest this might seem like a bit of a stretch but bear with me. I need to get in as many game references as I can!
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Help your users", canvas, dimensions);

        var text = textTransitions[currentTransition];

        canvas.DrawString(
            dimensions,
            text,
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var image = images[currentTransition];

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Bottom - imageHeight - 80, imageWidth, imageHeight);

        base.Render(canvas, dimensions);
    }
}
