using Orbit.Engine;

namespace AirHockey.GameObjects;

public class Paddle : GameObject
{
    private float x = -1;
    private float y = -1;

    public Color Color { get; set; }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        if (x == -1)
        {
            x = dimensions.Center.X;
        }

        if (y == -1)
        {
            y = 60;
        }

        canvas.FillColor = Color;
        canvas.FillCircle(x, y, 40);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);
    }

    public void UpdatePlayerState(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
