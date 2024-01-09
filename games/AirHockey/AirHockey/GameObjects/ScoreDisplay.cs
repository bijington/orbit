using Orbit.Engine;

namespace AirHockey.GameObjects;

public class ScoreDisplay : GameObject
{
    private float alpha = 0f;
    private const float FadeOutDuration = 5000f;

    private int currentScoreOne = 0;
    private int currentScoreTwo = 0;

    private readonly PlayerStateManager playerStateManager;

    public int ScoreIndex { get; set; }

    public ScoreDisplay(PlayerStateManager playerStateManager)
    {
        this.playerStateManager = playerStateManager;
    }

    public override void Render(Microsoft.Maui.Graphics.ICanvas canvas, Microsoft.Maui.Graphics.RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Alpha = alpha;
        canvas.Font = Microsoft.Maui.Graphics.Font.Default;
        canvas.FontColor = Colors.Black;
        canvas.FontSize = 200;
        
        if (ScoreIndex == 0)
        {
            canvas.DrawString(
                this.playerStateManager.ScoreState.ScoreOne.ToString(),
                new Rect(0, dimensions.Center.Y, dimensions.Width, dimensions.Height / 2),
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds
            );
        }
        else
        {
            canvas.DrawString(
                this.playerStateManager.ScoreState.ScoreTwo.ToString(),
                new Rect(0, 0, dimensions.Width, dimensions.Height / 2),
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds
            );
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (alpha > 0)
        {
            alpha -= (float)millisecondsSinceLastUpdate / FadeOutDuration;
        }

        if (currentScoreOne != this.playerStateManager.ScoreState.ScoreOne)
        {
            currentScoreOne = this.playerStateManager.ScoreState.ScoreOne;
            alpha = 1f;
        }

        if (currentScoreTwo != this.playerStateManager.ScoreState.ScoreTwo)
        {
            currentScoreTwo = this.playerStateManager.ScoreState.ScoreTwo;
            alpha = 1f;
        }
    }
}