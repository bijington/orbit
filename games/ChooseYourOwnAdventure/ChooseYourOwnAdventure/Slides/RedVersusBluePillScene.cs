using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class RedVersusBluePillScene : VoteSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 2;

    public RedVersusBluePillScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
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
        var introduction = "You are approached by a person, they offer you the choice between a red and a blue pill. Will you take the red pill and choose to learn about building engine based games inside .NET MAUI, or will you remain in the contented experience of ordinary application styles with the blue pill?";

        canvas.Alpha = 1.0f;
        canvas.Font = Styling.Font;
        canvas.FontSize = (float)Styling.ScaledFontSize(0.05);
        canvas.FontColor = Styling.TitleColor;

        if (currentTransition >= 0)
        {
            canvas.DrawString(
                introduction,
                new RectF(20, 20, dimensions.Width - 40, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Top,
                TextFlow.OverflowBounds);
        }

        if (currentTransition >= 1)
        {
            canvas.Alpha = 1.0f;
            canvas.Font = Styling.Font;
            canvas.FontSize = (float)Styling.ScaledFontSize(0.3);

            canvas.FontColor = Colors.Red;
            canvas.DrawString(
                Option1VoteCount.ToString(),
                new RectF(0, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Bottom,
                TextFlow.OverflowBounds);

            canvas.FontColor = Colors.Blue;
            canvas.DrawString(
                Option2VoteCount.ToString(),
                new RectF(dimensions.Width / 2, 0, dimensions.Width / 2, dimensions.Height),
                HorizontalAlignment.Center,
                VerticalAlignment.Bottom,
                TextFlow.OverflowBounds);
        }

        base.Render(canvas, dimensions);
    }
}

