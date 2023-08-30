using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class FreeBoarder : GameObject
{
    private readonly Microsoft.Maui.Graphics.IImage[] images;
    private readonly ControllerManager controllerManager;
    private float y = float.NaN;
    private int imageIndex = 0;
    private float riverTop;
    private float riverBottom;

    public Color Color { get; set; }

    public float RiverHeight { get; set; }

    public FreeBoarder(ControllerManager controllerManager)
    {
        images = new[]
        {
            LoadImage("boarder_right.png"),
            LoadImage("boarder_up.png"),
            LoadImage("boarder_down.png")
        };

        this.controllerManager = controllerManager;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var image = images[imageIndex];

        riverTop = (dimensions.Height - RiverHeight) / 2;
        riverBottom = dimensions.Height - (RiverHeight / 2);

        if (float.IsNaN(y))
        {
            y = dimensions.Center.Y;
        }

        canvas.DrawImage(
            image,
            100,
            y,
            image.Width,
            image.Height);
    }

    private readonly float movementWeight = 0.5f;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (controllerManager.CurrentPressedButton == ControllerButton.Up)
        {
            y = Math.Clamp(y - (movementWeight * (float)millisecondsSinceLastUpdate), riverTop, riverBottom);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.Down)
        {
            y = Math.Clamp(y + (movementWeight * (float)millisecondsSinceLastUpdate), riverTop, riverBottom);
        }

        if (controllerManager.CurrentPressedButton == ControllerButton.Up)
        {
            imageIndex = 1;
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.Down)
        {
            imageIndex = 2;
        }
        else
        {
            imageIndex = 0;
        }
    }
}
