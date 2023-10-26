using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class UpdateScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 1;

    public UpdateScene(Pointer pointer) : base(pointer)
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
        Styling.RenderTitle("Update state", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Converts the following:

  - Processed user input
    
  - Time since last update

- Modifies state

- Keep display independent",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var y = dimensions.Height * 0.18f;
        var availableVerticalSpace = dimensions.Height - 2 * y;

        canvas.StrokeSize = 4f;
        canvas.StrokeColor = Styling.Secondary;

        var width = dimensions.Width / 4;

        if (currentTransition == 1)
        {
            DrawDevice(canvas, dimensions.Center.X + 40, y, width - 80, availableVerticalSpace);
            DrawDevice(canvas, dimensions.Center.X + 40 + width, y, width * 0.75f - 80, availableVerticalSpace * 0.75f);
        }

        base.Render(canvas, dimensions);
    }

    private static void DrawDevice(ICanvas canvas, float x, float y, float width, float height)
    {
        canvas.StrokeSize = 4f;
        canvas.StrokeColor = Styling.Secondary;
        canvas.StrokeDashPattern = null;

        var rectangleBounds = new RectF(x, y, width, height);
        canvas.DrawRectangle(rectangleBounds);

        canvas.StrokeDashPattern = new[] { 5f, 5f, 5f, 5f };
        canvas.DrawLine(rectangleBounds.X, rectangleBounds.Center.Y, rectangleBounds.Center.X, rectangleBounds.Center.Y);
        canvas.DrawLine(rectangleBounds.Center.X, rectangleBounds.Y, rectangleBounds.Center.X, rectangleBounds.Center.Y);
    }
}
