using Orbit.Engine;

namespace ChooseYourOwnAdventure;

public class Bat : GameObject
{
    private readonly Microsoft.Maui.Graphics.IImage[] images;
    private int imageIndex;
    private double imageDisplayDuration = 200;
    private double elapsedMilliseconds;

    public Bat()
    {
        images = [LoadImage("bat1.png"), LoadImage("bat2.png")];
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.DrawImage(images[imageIndex], Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        elapsedMilliseconds += millisecondsSinceLastUpdate;

        if (elapsedMilliseconds > imageDisplayDuration)
        {
            elapsedMilliseconds = 0;

            imageIndex++;

            if (imageIndex >= images.Length)
            {
                imageIndex = 0;
            }
        }
    }
}
