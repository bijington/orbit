using Orbit.Engine;

namespace Orbit.GameObjects;

public class ScoreDisplay : GameObject
{
    private readonly StatisticsManager statisticsManager;
    private int score;
    private int lastScore;
    private int change;
    private float alpha;

    public ScoreDisplay(StatisticsManager statisticsManager)
	{
        this.statisticsManager = statisticsManager;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.FontSize = 20;

        canvas.DrawString(
            score.ToString(),
            20,
            20,
            dimensions.Width - 40,
            dimensions.Height - 40,
            HorizontalAlignment.Right,
            VerticalAlignment.Top);

        if (change != 0)
        {
            canvas.Alpha = alpha;

            canvas.DrawString(
                change.ToString(),
                20,
                40,
                dimensions.Width - 40,
                dimensions.Height - 40,
                HorizontalAlignment.Right,
                VerticalAlignment.Top);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        score = statisticsManager.Score;

        if (lastScore != score)
        {
            alpha = 1f;

            change = score - lastScore;

            lastScore = score;
        }
        if (change != 0)
        {
            alpha -= (float)millisecondsSinceLastUpdate;
        }
        if (alpha == 0)
        {
            change = 0;
        }
    }
}
