using Orbit.Engine;

namespace ChooseYourOwnAdventure;

public class WorldMap : GameObject
{
    private readonly IDictionary<TileTypes, Microsoft.Maui.Graphics.IImage> images = new Dictionary<TileTypes, Microsoft.Maui.Graphics.IImage>();
    private readonly Microsoft.Maui.Graphics.IImage character;
    private const int columns = 30;
    private const int rows = 26;

    private TileTypes[,] tiles = 
    {
        { TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.GreenTopLeft, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTop, TileTypes.GreenTopRight, TileTypes.Water, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenTopLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenTopRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.GreenBottomLeft, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.Green, TileTypes.GreenRight, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.GreenBottomLeft, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.GreenBottom, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water },
        { TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water, TileTypes.Water },
    };

    public WorldMap()
    {
        var tileTypes = new[]
        {
            TileTypes.Green,
            TileTypes.Mountain,
            TileTypes.Tree,
            TileTypes.Water,
            TileTypes.Water1,
            TileTypes.Water2,
            TileTypes.Step,
            TileTypes.BottomEdge,
            TileTypes.RightEdge,
            TileTypes.LeftEdge,
            TileTypes.TopEdge,
            TileTypes.FireOne,
            TileTypes.FireTwo,
        };

        try
        {
            foreach (var type in tileTypes)
            {
                if (type == TileTypes.None)
                {
                    continue;
                }

                var image = LoadImage($"{type.ToString().ToLower()}.png");
                images.Add(type, image);
            }
        }
        catch (Exception ex)
        {

        }

        character = LoadImage("character.png");
    }

    private TileTypes waterOverlayIndex = TileTypes.Water1;
    private int fireIndex = 11;
    private double elapsedMilliseconds = 0;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        elapsedMilliseconds += millisecondsSinceLastUpdate;

        if (elapsedMilliseconds > 500)
        {
            elapsedMilliseconds = 0;

            if (waterOverlayIndex == TileTypes.Water1)
            {
                waterOverlayIndex = TileTypes.Water2;
            }
            else
            {
                waterOverlayIndex = TileTypes.Water1;
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
                    var tileType = this.tiles[y, x];

                    RenderTile(tileType, TileTypes.Green, canvas, x, y, tileSize);

                    if (tileType.HasFlag(TileTypes.Water))
                    {
                        canvas.DrawImage(images[TileTypes.Water], x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(images[waterOverlayIndex], x * tileSize, y * tileSize, tileSize, tileSize);
                    }

                    RenderTile(tileType, TileTypes.TopEdge, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.BottomEdge, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.LeftEdge, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.RightEdge, canvas, x, y, tileSize);
                    // if (x is 0 or columns - 1 || y is 0 or rows - 1)
                    // {
                    //     canvas.DrawImage(images[3], x * tileSize, y * tileSize, tileSize, tileSize);

                    //     // canvas.FillColor = Colors.Blue;
                    //     // canvas.FillRectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    //     canvas.DrawImage(images[waterOverlayIndex], x * tileSize, y * tileSize, tileSize, tileSize);
                    //     if (y is rows - 1 && x is > 0 and < columns - 1)
                    //     {
                    //         canvas.DrawImage(images[7], x * tileSize, y * tileSize, tileSize, tileSize);
                    //     }
                    //     else if (y is > 0 and < rows - 1 && x is columns - 1)
                    //     {
                    //         canvas.DrawImage(images[8], x * tileSize, y * tileSize, tileSize, tileSize);
                    //     }
                    //     else if (y is > 0 and < rows - 1 && x is 0)
                    //     {
                    //         canvas.DrawImage(images[9], x * tileSize, y * tileSize, tileSize, tileSize);
                    //     }
                    //     else if (y is 0 && x is > 0 and < columns - 1)
                    //     {
                    //         canvas.DrawImage(images[10], x * tileSize, y * tileSize, tileSize, tileSize);
                    //     }
                    // }
                    // else
                    // {
                    //     // canvas.FillColor = Colors.Green;
                    //     // canvas.FillRectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                    //     canvas.DrawImage(image, x * tileSize, y * tileSize, tileSize, tileSize);
                    // }

                    // if (y is 3 && x is >= 2 and <= columns - 3)
                    // {
                    //     canvas.DrawImage(images[6], x * tileSize, y * tileSize, tileSize, tileSize);
                    // }
                    // if (x is 2 or columns - 3 && y is 2 or rows - 3)
                    // {
                    //     canvas.DrawImage(tree, x * tileSize, y * tileSize, tileSize, tileSize);
                    // }
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

            //canvas.DrawImage(images[fireIndex], 4 * tileSize, 4 * tileSize, tileSize, tileSize);
        }
        catch (Exception ex)
        {

        }
        
    }

    private void RenderTile(TileTypes currentType, TileTypes tileToRender, ICanvas canvas, int x, int y, float tileSize)
    {
        if (currentType.HasFlag(tileToRender))
        {
            canvas.DrawImage(images[tileToRender], x * tileSize, y * tileSize, tileSize, tileSize);
        }
    }
}

[Flags]
internal enum TileTypes
{
    None = 0,
    Green = 1,
    Mountain = 2,
    Tree = 4,
    Water = 8,
    Water1 = 16,
    Water2 = 32,
    Step = 64,
    BottomEdge = 128,
    RightEdge = 256,
    LeftEdge = 512,
    TopEdge = 1024,
    FireOne = 2048,
    FireTwo = 4096,

    GreenLeft = Green | LeftEdge,
    GreenRight = Green | RightEdge,
    GreenBottom = Green | BottomEdge,
    GreenTop = Green | TopEdge,
    GreenTopLeft = Green | TopEdge | LeftEdge,
    GreenTopRight = Green | TopEdge | RightEdge,
    GreenBottomLeft = Green | BottomEdge | LeftEdge,
    GreenBottomRight = Green | BottomEdge | RightEdge,
}