namespace BuildingGames.Scenes;

public class HandlingInputScene : SlideSceneBase
{
    private float opacity = 1.0f;
    private readonly ControllerManager controllerManager;

    public override bool CanProgress => opacity < 0.6f;

    public HandlingInputScene(ControllerManager controllerManager)
    {
        this.controllerManager = controllerManager;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var font = Styling.Font;

        canvas.Font = font;
        canvas.FontSize = 80;
        canvas.FontColor = Colors.White;
        canvas.SetShadow(new SizeF(5, 5), 5, Colors.Black);

        float y = dimensions.Height * 0.15f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            "Handling input",
            font,
            80,
            new PointF(40, y),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        y += 200f;

        canvas.Alpha = opacity;
        canvas.Font = font;
        canvas.FontSize = 40;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Keyboard",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Touch",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Mouse",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Accelerometer",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        y += 100f;

        canvas.Alpha = 1f;

        canvas.DrawString(
            new RectF(40, y, dimensions.Width - 80, dimensions.Height),
            " - Game controller",
            font,
            40,
            new PointF(40, y),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (this.controllerManager.CurrentPressedButton == ControllerButton.Accept)
        {
            opacity = 0.5f;
        }
    }
}
