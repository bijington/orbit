using Orbit.Engine;

namespace AirHockey.GameObjects;

public class ScoreDisplay : GameObject
{
    private float alpha = 0f;
    private const float FadeOutDuration = 5000f;

    private readonly GameStateManager gameStateManager;

    public ScoreDisplay(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    private void ScoreChanged()
    {
        alpha = 1f;
    }

    public override void Render(Microsoft.Maui.Graphics.ICanvas canvas, Microsoft.Maui.Graphics.RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Alpha = alpha;
        //canvas.Draw
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (alpha > 0)
        {
            alpha -= (float)millisecondsSinceLastUpdate / FadeOutDuration;
        }
    }
}