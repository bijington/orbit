namespace Orbit;

public class GameObject : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();
        canvas.ResetState();

        Render(canvas, dirtyRect);

        canvas.RestoreState();
    }

    public virtual void Render(ICanvas canvas, RectF dirtyRect)
    {

    }
}
