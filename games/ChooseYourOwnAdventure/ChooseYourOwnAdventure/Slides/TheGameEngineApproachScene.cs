using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TheGameEngineApproachScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 5;

    private readonly IList<string> textTransitions;

    public TheGameEngineApproachScene(Pointer pointer) : base(pointer)
	{
        textTransitions = new List<string>
        {
            "- Game loop",
            @"
   - Runs continuously",
            @"
   - Processes user input",
            @"
   - Updates game state",
            @"
   - Renders game",
            @"
   - Waits"
        };
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
        Styling.RenderTitle("The game engine approach", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            string.Join(Environment.NewLine, textTransitions.Take(currentTransition + 1)),
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        canvas.StrokeColor = Styling.TitleColor;
        canvas.StrokeSize = 10;
        canvas.FillColor = Styling.Primary;

        if (currentTransition > 0)
        {
            var loopBounds = new RectF(
                dimensions.Center.X,
                dimensions.Height * 0.2f,
                dimensions.Width / 4,
                dimensions.Height / 1.5f);
            canvas.DrawRoundedRectangle(loopBounds, 30);

            const float boxWidth = 400;
            float boxHeight = loopBounds.Height / 7f;
            float halfWidth = boxWidth / 2;

            if (currentTransition > 1)
            {
                DrawBox("Process input", loopBounds.Right - halfWidth, loopBounds.Y + boxHeight, boxWidth, boxHeight, canvas);
            }

            if (currentTransition > 2)
            {
                DrawBox("Update", loopBounds.Right - halfWidth, loopBounds.Y + boxHeight * 3, boxWidth, boxHeight, canvas);
            }

            if (currentTransition > 3)
            {
                DrawBox("Render", loopBounds.Right - halfWidth, loopBounds.Y + boxHeight * 5, boxWidth, boxHeight, canvas);
            }

            if (currentTransition > 4)
            {
                DrawBox("Wait", loopBounds.X - halfWidth, loopBounds.Center.Y - boxHeight / 2, boxWidth, boxHeight, canvas);
            }
        }

        base.Render(canvas, dimensions);
    }

    private void DrawBox(
        string text,
        float x,
        float y,
        float width,
        float height,
        ICanvas canvas)
    {
        var bounds = new RectF(x, y, width, height);

        canvas.FillRoundedRectangle(bounds, 16);

        canvas.DrawRoundedRectangle(bounds, 16);

        canvas.DrawString(
            bounds,
            text,
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.CodeFont,
            (float)Styling.ScaledFontSize(0.035),
            new PointF(x, y),
            HorizontalAlignment.Center,
            VerticalAlignment.Center);
    }
}
