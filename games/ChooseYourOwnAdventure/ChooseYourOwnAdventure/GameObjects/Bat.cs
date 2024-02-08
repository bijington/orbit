using Orbit.Engine;

namespace ChooseYourOwnAdventure;

public class Bat : GameObject
{
    private Sprite batSprite;

    public Bat()
    {
        batSprite = new Sprite(imageNames: ["bat1.png", "bat2.png"], imageDisplayDuration: 200);

        Add(batSprite);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        // Need to clean this up.
        batSprite.Bounds = this.Bounds;

        base.Render(canvas, dimensions);
    }
}

public class Sprite : GameObject
{
    private readonly IReadOnlyList<Microsoft.Maui.Graphics.IImage> images;
    private readonly double imageDisplayDuration = 200;
    private int imageIndex;
    private double elapsedMilliseconds;
    private bool isRunning;

    public Sprite(IReadOnlyList<string> imageNames, double imageDisplayDuration, bool autoStart = true)
    {
        this.images = imageNames.Select(LoadImage).ToList();
        this.imageDisplayDuration = imageDisplayDuration;
        this.isRunning = autoStart;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.DrawImage(images[imageIndex], Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (this.isRunning is false)
        {
            return;
        }

        elapsedMilliseconds += millisecondsSinceLastUpdate;

        if (elapsedMilliseconds > imageDisplayDuration)
        {
            elapsedMilliseconds = 0;

            imageIndex++;

            if (imageIndex >= images.Count)
            {
                imageIndex = 0;
            }
        }
    }

    public void Stop()
    {
        elapsedMilliseconds = 0;
        imageIndex = 0;
        this.isRunning = false;
    }

    public void Pause()
    {
        this.isRunning = false;
    }

    public void Start()
    {
        this.isRunning = true;
    }
}
