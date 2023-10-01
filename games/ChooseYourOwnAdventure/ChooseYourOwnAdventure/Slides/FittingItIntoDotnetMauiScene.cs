using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class FittingItIntoDotnetMauiScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 3;

    private readonly IList<string> textTransitions;

    public FittingItIntoDotnetMauiScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
        textTransitions = new List<string>
        {
            @"
private void UpdateScene()
{
    if (gameState != GameState.Started)
    {
        return;
    }

    // Update
    CurrentScene.Update(timeSinceLastUpdate);

    // Render
    gameSceneView.Invalidate();

    // Wait
    dispatcher.DispatchDelayed(
        TimeSpan.FromMilliseconds(delayUntilNextUpdate),
        () =>
        {
            UpdateScene();
        });
}"
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
        Styling.RenderTitle("Fitting it into .NET MAUI", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            string.Join(Environment.NewLine, textTransitions.Take(currentTransition + 1)),
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.02),
            new PointF(40, dimensions.Height * 0.2f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        canvas.StrokeColor = Styling.TitleColor;
        canvas.StrokeSize = 10;
        canvas.FillColor = Styling.Primary;

        canvas.Alpha = 0.3f;

        var loopBounds = new RectF(
            dimensions.Center.X,
            dimensions.Height * 0.2f,
            dimensions.Width / 4,
            dimensions.Height / 1.5f);
        canvas.DrawRoundedRectangle(loopBounds, 30);

        const float boxWidth = 400;
        const float boxHeight = 100;
        float halfWidth = boxWidth / 2;

        DrawBox("Process input", loopBounds.Right - halfWidth, loopBounds.Y + boxHeight, boxWidth, boxHeight, canvas, currentTransition == 0);

        DrawBox("Update", loopBounds.Right - halfWidth, loopBounds.Y + boxHeight * 3, boxWidth, boxHeight, canvas, currentTransition == 1);

        DrawBox("Render", loopBounds.Right - halfWidth, loopBounds.Y + boxHeight * 5, boxWidth, boxHeight, canvas, currentTransition == 2);

        DrawBox("Wait", loopBounds.X - halfWidth, loopBounds.Center.Y - boxHeight / 2, boxWidth, boxHeight, canvas, currentTransition == 3);

        canvas.Alpha = 1f;

        base.Render(canvas, dimensions);
    }

    private void DrawBox(
        string text,
        float x,
        float y,
        float width,
        float height,
        ICanvas canvas,
        bool isCurrent)
    {
        var bounds = new RectF(x, y, width, height);

        canvas.Alpha = 1.0f;

        canvas.FillRoundedRectangle(bounds, 16);

        var alpha = isCurrent ? 1.0f : 0.3f;
        canvas.Alpha = alpha;

        canvas.DrawRoundedRectangle(bounds, 16);

        canvas.DrawString(
            bounds,
            text,
            Styling.TitleColor,
            Colors.Transparent,
            alpha,
            Styling.CodeFont,
            (float)Styling.ScaledFontSize(0.04),
            new PointF(x, y),
            HorizontalAlignment.Center,
            VerticalAlignment.Center);
    }
}
