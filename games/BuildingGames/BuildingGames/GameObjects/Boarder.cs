using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class Boarder : GameObject
{
    private readonly Microsoft.Maui.Graphics.IImage[] images;
    private readonly ControllerManager controllerManager;
    private int row = 1;
    private int imageIndex = 0;
    const int rows = 3;
    private bool handlingKeyPress = false;

    public Color Color { get; set; }

    public float RiverHeight { get; set; }

    public Boarder(ControllerManager controllerManager)
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

        var y = (RiverHeight / rows) * (row + 0.5f) - (image.Height / 2) + ((dimensions.Height - RiverHeight) / 2);

        canvas.DrawImage(
            image,
            100,
            y,
            image.Width,
            image.Height);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        // Dampen the control.
        if (controllerManager.CurrentPressedButton == ControllerButton.Up &&
            !handlingKeyPress)
        {
            handlingKeyPress = true;
            row = Math.Max(0, row - 1);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.Down &&
            !handlingKeyPress)
        {
            handlingKeyPress = true;
            row = Math.Min(2, row + 1);
        }
        else if (controllerManager.CurrentPressedButton == ControllerButton.None)
        {
            handlingKeyPress = false;
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
