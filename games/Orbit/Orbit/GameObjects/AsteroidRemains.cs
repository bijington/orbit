using Orbit.Engine;

namespace Orbit.GameObjects;

public class AsteroidRemains : GameObject
{
    Microsoft.Maui.Graphics.IImage image;
    private float opacity = 1f;
    private float opacityDecrements = 0.03f;

    public AsteroidRemains()
    {
        image = LoadImage("asteroid_remains.png");
    }

    public void SetBounds(RectF bounds)
    {
        Bounds = bounds;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        this.opacity -= opacityDecrements;

        if (this.opacity <= 0)
        {
            this.CurrentScene.Remove(this);
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Alpha = opacity;
        canvas.DrawImage(image, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
    }
}
