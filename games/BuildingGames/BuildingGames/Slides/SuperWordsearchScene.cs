using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class SuperWordsearchScene : SlideSceneBase
{
	private int currentTransition = 0;
	private const int transitions = 1;
    private float textY = float.NaN;

	public SuperWordsearchScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
    {
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

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("A few years ago in a town not far from here....", canvas, dimensions);

        if (float.IsNaN(textY))
        {
            textY = dimensions.Height;
        }

        if (currentTransition > 0)
        {
            canvas.DrawString(
                dimensions,
                @"
Super
Wordsearch",
                Styling.TitleColor,
                Colors.Transparent,
                1,
                Styling.Font,
                175,
                new PointF(0, textY),
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (currentTransition > 0)
        {
            textY--;
        }
    }
}
