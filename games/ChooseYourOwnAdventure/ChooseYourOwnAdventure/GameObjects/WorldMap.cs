using Orbit.Engine;

namespace ChooseYourOwnAdventure;

public class WorldMap : GameObject
{
    private readonly IList<Microsoft.Maui.Graphics.IImage> images;
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly Microsoft.Maui.Graphics.IImage tree;
    private readonly Microsoft.Maui.Graphics.IImage character;
    private const int columns = 30;
    private const int rows = 20;

    public WorldMap()
    {
        images = new List<Microsoft.Maui.Graphics.IImage>
        {
            LoadImage("green.png"),
            LoadImage("mountain.png"),
            LoadImage("tree.png"),
            LoadImage("water.png"),
            LoadImage("water1.png"),
            LoadImage("water2.png"),
            LoadImage("step.png"),
        };

        image = LoadImage("green.png");
        tree = LoadImage("tree.png");
        character = LoadImage("character.png");
    }

    private int waterOverlayIndex = 4;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        try
        {
            base.Render(canvas, dimensions);

            var width = dimensions.Width / columns;
            var height = dimensions.Height / rows;

            float tileSize = MathF.Min(height, width);

            for (var x = 0; x < columns; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    if (x is 0 or columns - 1 || y is 0 or rows - 1)
                    {
                        canvas.DrawImage(images[3], x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(images[waterOverlayIndex], x * tileSize, y * tileSize, tileSize, tileSize);
                    }
                    else
                    {
                        canvas.DrawImage(image, x * tileSize, y * tileSize, tileSize, tileSize);
                    }

                    if (y is 3 && x is >= 2 and <= columns - 1)
                    {
                        canvas.DrawImage(images[6], x * tileSize, y * tileSize, tileSize, tileSize);
                    }
                    // if (y % 2 == 0 && x % 3 == 0)
                    // {
                    //     canvas.DrawImage(tree, x * tileSize, y * tileSize, tileSize, tileSize);
                    // }
                    // else
                    // {
                    //     canvas.DrawImage(images[1], x * tileSize, y * tileSize, tileSize, tileSize);
                    // }
                }
            }

            canvas.DrawImage(character, 3 * tileSize, 3 * tileSize, tileSize, tileSize);
        }
        catch (Exception ex)
        {

        }
        
    }
}