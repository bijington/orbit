using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class Slide03 : SlideSceneBase
{
	private int currentTransition = 0;
	private const int transitions = 1;

	public Slide03(Pointer pointer) : base(pointer)
    {
	}

    public override void Progress()
    {
        currentTransition++;

        // If we are complete then fire the Next event.
        if (currentTransition == transitions)
        {
            base.Progress();
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        Styling.RenderTitle("A few years ago in a town not far from here....", canvas, dimensions);

        if (currentTransition > 0)
        {
            canvas.DrawString(
                dimensions,
                "A wordsearch application was born",
                Styling.TitleColor,
                Colors.Transparent,
                1,
                Styling.Font,
                25,
                new PointF(0, dimensions.Height * 0.75f),
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }
    }
}
