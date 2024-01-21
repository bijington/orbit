using Orbit.Engine;

namespace ChooseYourOwnAdventure;

public class WorldMap : GameObject
{
    private readonly IDictionary<TileTypes, Microsoft.Maui.Graphics.IImage> images = new Dictionary<TileTypes, Microsoft.Maui.Graphics.IImage>();
    private readonly Microsoft.Maui.Graphics.IImage character;
    private const int columns = 30;
    private const int rows = 20;

    private TileTypes[,] tiles = 
    {
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.GrTL, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrTL, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Post, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrTR, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Hill, TileTypes.Path, TileTypes.Hill, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Hill, TileTypes.Path, TileTypes.Hill, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Hill, TileTypes.Path, TileTypes.Hill, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.GrBL, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrBR, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.GrBL, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
    };

    public WorldMap()
    {
        var tileTypes = new[]
        {
            TileTypes.Gree,
            TileTypes.Moun,
            TileTypes.Tree,
            TileTypes.Wate,
            TileTypes.Wat1,
            TileTypes.Wat2,
            TileTypes.Step,
            TileTypes.BoEd,
            TileTypes.RiEd,
            TileTypes.LeEd,
            TileTypes.ToEd,
            TileTypes.Fir1,
            TileTypes.Fir2,
            TileTypes.Door,
            TileTypes.Wall,
            TileTypes.Roof,
            TileTypes.Sign,
        };

        try
        {
            foreach (var type in tileTypes)
            {
                if (type == TileTypes.None)
                {
                    continue;
                }

                var image = LoadImage($"t_{type.ToString().ToLower()}.png");
                images.Add(type, image);
            }
        }
        catch (Exception ex)
        {

        }

        character = LoadImage("character.png");
    }

    private TileTypes WateOverlayIndex = TileTypes.Wat1;
    private int fireIndex = 11;
    private double elapsedMilliseconds = 0;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        elapsedMilliseconds += millisecondsSinceLastUpdate;

        if (elapsedMilliseconds > 500)
        {
            elapsedMilliseconds = 0;

            if (WateOverlayIndex == TileTypes.Wat1)
            {
                WateOverlayIndex = TileTypes.Wat2;
            }
            else
            {
                WateOverlayIndex = TileTypes.Wat1;
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

                    RenderTile(tileType, TileTypes.Gree, canvas, x, y, tileSize);

                    if (tileType.HasFlag(TileTypes.Wate))
                    {
                        canvas.DrawImage(images[TileTypes.Wate], x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(images[WateOverlayIndex], x * tileSize, y * tileSize, tileSize, tileSize);
                    }

                    RenderTile(tileType, TileTypes.ToEd, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.BoEd, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.LeEd, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.RiEd, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.Step, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.Moun, canvas, x, y, tileSize);
                    RenderTile(tileType, TileTypes.Sign, canvas, x, y, tileSize);
                }
            }

            canvas.DrawImage(character, 4 * tileSize, 4 * tileSize, tileSize, tileSize);

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
    Gree = 1,
    Moun = 2,
    Tree = 4,
    Wate = 8,
    Wat1 = 16,
    Wat2 = 32,
    Step = 64,
    BoEd = 128,
    RiEd = 256,
    LeEd = 512,
    ToEd = 1024,
    Fir1 = 2048,
    Fir2 = 4096,
    Door = 8192,
    Wall = 16384,
    Roof = 32768,
    Sign = 65536,

    GrLe = Gree | LeEd,
    GrRi = Gree | RiEd,
    GrBo = Gree | BoEd,
    GrTo = Gree | ToEd,
    GrTL = Gree | ToEd | LeEd,
    GrTR = Gree | ToEd | RiEd,
    GrBL = Gree | BoEd | LeEd,
    GrBR = Gree | BoEd | RiEd,

    Path = Step | Gree,
    Hill = Moun | Gree,
    Post = Sign | Gree,
}