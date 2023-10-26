using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class SummaryScene : SlideSceneBase
{
    private readonly Decisions decisions;

    public SummaryScene(Pointer pointer, Decisions decisions) : base(pointer)
    {
        this.decisions = decisions;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Game complete", canvas, dimensions);
        // So how did you do?

        // TODO: stretch goal - render a map.
        var decisionHistory = this.decisions.History;

        string summary = "- You chose the following:";

        foreach (var decision in decisionHistory)
        {
            summary += @$"

- {decision}";
        }

        canvas.DrawString(
            dimensions,
            summary,
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
