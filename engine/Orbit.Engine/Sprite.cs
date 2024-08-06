using System.Diagnostics;
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
    
    /// <summary>
    /// Gets whether the sprite is running.
    /// </summary>
    public bool IsRunning { get; private set; }

    /// <summary>
    /// Creates a new instance of <see cref="Sprite"/> which accepts the names of images to load.
    /// </summary>
    /// <param name="imageNames">The names of the images to display, in order of sequence to be displayed.</param>
    /// <param name="imageDisplayDuration">How long each image should be displayed for before transitioning to the next image in the sequence.</param>
    /// <param name="autoStart">Whether the sprite animation should start automatically.</param>
    /// <param name="playMode">How the sprite will animate.</param>
    public Sprite(IReadOnlyList<string> imageNames, double imageDisplayDuration, bool autoStart = true, PlayModeType playMode = PlayModeType.Loop)
    {
        this.images = imageNames.Select(LoadImage).ToList();
        this.imageDisplayDuration = imageDisplayDuration;
        this.IsRunning = autoStart;
        this.PlayMode = playMode;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Sprite"/> which accepts the names of images to load.
    /// </summary>
    /// <param name="images">The images to display, in order of sequence to be displayed.</param>
    /// <param name="imageDisplayDuration">How long each image should be displayed for before transitioning to the next image in the sequence.</param>
    /// <param name="autoStart">Whether the sprite animation should start automatically.</param>
    /// <param name="playMode">How the sprite will animate.</param>
    public Sprite(IReadOnlyList<Microsoft.Maui.Graphics.IImage> images, double imageDisplayDuration, bool autoStart = true, PlayModeType playMode = PlayModeType.Loop)
    {
        this.images = images;
        this.imageDisplayDuration = imageDisplayDuration;
        this.IsRunning = autoStart;
        this.PlayMode = playMode;
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

        if (this.IsRunning is false)
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

            if (imageIndex == images.Count)
            {
                imageIndex = 0;

                if (PlayMode == PlayModeType.Single)
                {
                    Stop();
                }
            }
        }
    }

    /// <summary>
    /// Stops the sprite animation and reverts back to the default image.
    /// </summary>
    public void Stop()
    {
        elapsedMilliseconds = 0;
        imageIndex = 0;
        this.IsRunning = false;
    }

    /// <summary>
    /// Pauses the sprite animation.
    /// </summary>
    public void Pause()
    {
        this.IsRunning = false;
    }

    /// <summary>
    /// Starts the sprite animation.
    /// </summary>
    public void Start()
    {
        this.IsRunning = true;
    }
    
    /// <summary>
    /// Gets the <see cref="PlayMode"/> determining how the sprite will animate.
    /// </summary>
    public PlayModeType PlayMode { get; }

    /// <summary>
    /// Gets the mode in which a sprite will animate.
    /// </summary>
    public enum PlayModeType
    {
        /// <summary>
        /// The sprite will animate through the full sequence once and then stop.
        /// </summary>
        Single,
        
        /// <summary>
        /// The sprite will animate indefinitely.
        /// </summary>
        Loop,
        
        /// <summary>
        /// The sprite will animate forwards and then backwards through the sequence.
        /// </summary>
        Reverse
    }
}
