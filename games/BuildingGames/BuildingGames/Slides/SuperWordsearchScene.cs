using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class SuperWordsearchScene : SlideSceneBase
{
	private int currentTransition = 0;
	private const int transitions = 2;
    private float fontSize = 1000f;

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
        if (currentTransition == 0)
        {
            Styling.RenderTitle("A few years ago in a town not far from here....", canvas, dimensions);
        }
        else if (currentTransition == 1)
        {
            canvas.Alpha = 1.0f;
            canvas.Font = Styling.Font;
            canvas.FontSize = fontSize;
            canvas.FontColor = Styling.TitleColor;

            canvas.DrawString(
                @"Super
Wordsearch",
                dimensions,
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds);
        }
        else if (currentTransition == 2)
        {
            canvas.DrawString(
                dimensions,
                @"
Demo time!",
                Styling.TitleColor,
                Colors.Transparent,
                1,
                Styling.Font,
                (float)Styling.ScaledFontSize(0.05),
                new PointF(40, dimensions.Height * 0.2f),
                HorizontalAlignment.Center,
                VerticalAlignment.Center);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (currentTransition == 1)
        {
            fontSize -= 0.5f;

            if (fontSize <= 0)
            {
                currentTransition++;
            }
        }
    }
}
