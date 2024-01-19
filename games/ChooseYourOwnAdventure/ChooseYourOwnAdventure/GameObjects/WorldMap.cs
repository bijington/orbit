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
            LoadImage("bottomedge.png"),
            LoadImage("rightedge.png"),
            LoadImage("leftedge.png"),
            LoadImage("topedge.png"),
            LoadImage("fireone.png"),
            LoadImage("firetwo.png"),
        };

        image = LoadImage("green.png");
        tree = LoadImage("tree.png");
        character = LoadImage("character.png");
    }

    private int waterOverlayIndex = 4;
    private int fireIndex = 11;
    private double elapsedMilliseconds = 0;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        elapsedMilliseconds += millisecondsSinceLastUpdate;

        if (elapsedMilliseconds > 500)
        {
            elapsedMilliseconds = 0;
            waterOverlayIndex++;
            if (waterOverlayIndex > 5)
            {
                waterOverlayIndex = 4;
            }

            fireIndex++;
            if (fireIndex > 12)
            {
                fireIndex = 11;
            }
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        try
        {
            base.Render(canvas, dimensions);

            var width = dimensions.Width / columns;
            var height = dimensions.Height / rows;

            float tileSize = (int)MathF.Min(height, width);

            for (var x = 0; x < columns; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    if (x is 0 or columns - 1 || y is 0 or rows - 1)
                    {
                        canvas.DrawImage(images[3], x * tileSize, y * tileSize, tileSize, tileSize);

                        // canvas.FillColor = Colors.Blue;
                        // canvas.FillRectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(images[waterOverlayIndex], x * tileSize, y * tileSize, tileSize, tileSize);
                        if (y is rows - 1 && x is > 0 and < columns - 1)
                        {
                            canvas.DrawImage(images[7], x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                        else if (y is > 0 and < rows - 1 && x is columns - 1)
                        {
                            canvas.DrawImage(images[8], x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                        else if (y is > 0 and < rows - 1 && x is 0)
                        {
                            canvas.DrawImage(images[9], x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                        else if (y is 0 && x is > 0 and < columns - 1)
                        {
                            canvas.DrawImage(images[10], x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                    }
                    else
                    {
                        // canvas.FillColor = Colors.Green;
                        // canvas.FillRectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(image, x * tileSize, y * tileSize, tileSize, tileSize);
                    }

                    if (y is 3 && x is >= 2 and <= columns - 3)
                    {
                        canvas.DrawImage(images[6], x * tileSize, y * tileSize, tileSize, tileSize);
                    }
                    if (x is 2 or columns - 3 && y is 2 or rows - 3)
                    {
                        canvas.DrawImage(tree, x * tileSize, y * tileSize, tileSize, tileSize);
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

            canvas.DrawImage(images[fireIndex], 4 * tileSize, 4 * tileSize, tileSize, tileSize);
        }
        catch (Exception ex)
        {

        }
        
    }
}