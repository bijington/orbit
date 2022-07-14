using System;
using Orbit.Engine;

namespace DrawingGame.GameObjects;

public class ColorPalette : GameObject
{
    private IList<Color> supportedColors = new List<Color>
    {
        Colors.Black,
        Colors.Red,
        Colors.Orange,
        Colors.Yellow,
        Colors.Green,
        Colors.Blue,
        Colors.Indigo,
        Colors.Violet,
        Colors.White
    };

    private readonly DrawingManager drawingManager;

    public ColorPalette(
        DrawingManager drawingManager)
    {
        this.drawingManager = drawingManager;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        drawingManager.SelectedColor = Colors.Red;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        int barHeight = (int)(dimensions.Height * 0.1);
        canvas.FillColor = Colors.PaleGoldenrod;
        canvas.FillRectangle(0, 0, dimensions.Width, barHeight);

        int spacing = 10;
        int height = barHeight - (2 * spacing);
        int width = height;

        for (int i = 0; i < supportedColors.Count; i++)
        {
            var color = supportedColors[i];

            canvas.FillColor = color;

            if (drawingManager.SelectedColor == color)
            {
                canvas.FillRoundedRectangle(spacing + i * (width + spacing), spacing, width, height, 10);
            }
            else
            {
                canvas.FillRoundedRectangle(spacing + i * (width + spacing) + spacing, spacing * 2, width - spacing, height - spacing, 10);
            }
        }
    }
}
