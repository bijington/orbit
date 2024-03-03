using System.Linq;

namespace Orbit.Engine;

/// <summary>
/// Provides the ability to render a sprite animation in a game.
/// </summary>
public class Sprite : GameObject
{
    private readonly IReadOnlyList<Microsoft.Maui.Graphics.IImage> images;
    private readonly double imageDisplayDuration;
    private int imageIndex;
    private double elapsedMilliseconds;
    private bool isRunning;

    /// <summary>
    /// Creates a new instance of <see cref="Sprite"/> which accepts the names of images to load.
    /// </summary>
    /// <param name="imageNames">The names of the images to display, in order of sequence to be displayed.</param>
    /// <param name="imageDisplayDuration">How long each image should be displayed for before transitioning to the next image in the sequence.</param>
    /// <param name="autoStart">Whether the sprite animation should start automatically.</param>
    public Sprite(IReadOnlyList<string> imageNames, double imageDisplayDuration, bool autoStart = true)
    {
        this.images = imageNames.Select(LoadImage).ToList();
        this.imageDisplayDuration = imageDisplayDuration;
        this.isRunning = autoStart;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Sprite"/> which accepts the names of images to load.
    /// </summary>
    /// <param name="images">The images to display, in order of sequence to be displayed.</param>
    /// <param name="imageDisplayDuration">How long each image should be displayed for before transitioning to the next image in the sequence.</param>
    /// <param name="autoStart">Whether the sprite animation should start automatically.</param>
    public Sprite(IReadOnlyList<Microsoft.Maui.Graphics.IImage> images, double imageDisplayDuration, bool autoStart = true)
    {
        this.images = images;
        this.imageDisplayDuration = imageDisplayDuration;
        this.isRunning = autoStart;
    }

    /// <inheritdoc />
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.DrawImage(images[imageIndex], Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height);
    }

    /// <inheritdoc />
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

            // TODO: Do we have looping modes? Basically:
            // - Loop
            // - Reverse
            imageIndex = Math.Clamp(imageIndex + 1, 0, images.Count);
        }
    }

    /// <summary>
    /// Stops the sprite animation and reverts back to the default image.
    /// </summary>
    public void Stop()
    {
        elapsedMilliseconds = 0;
        imageIndex = 0;
        this.isRunning = false;
    }

    /// <summary>
    /// Pauses the sprite animation.
    /// </summary>
    public void Pause()
    {
        this.isRunning = false;
    }

    /// <summary>
    /// Starts the sprite animation.
    /// </summary>
    public void Start()
    {
        this.isRunning = true;
    }
}
