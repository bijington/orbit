using Orbit.Engine;

namespace ChooseYourOwnAdventure;

public class WorldMap : GameObject
{
    private readonly IDictionary<TileTypes, Microsoft.Maui.Graphics.IImage> images = new Dictionary<TileTypes, Microsoft.Maui.Graphics.IImage>();
    private readonly Microsoft.Maui.Graphics.IImage character;
    private readonly Bat bat;
    private const int columns = 30;
    private const int rows = 20;
    private TileTypes[] tileTypes;

    private TileTypes[,] tiles = 
    {
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.GrTL, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.GrTL, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTo, TileTypes.GrTR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrTL, TileTypes.GrTo, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Post, TileTypes.Gree, TileTypes.Post, TileTypes.Gree, TileTypes.Post, TileTypes.Gree, TileTypes.Post, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrTR, TileTypes.Wate, TileTypes.Wate, TileTypes.GrTL, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrTR, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.PaRi, TileTypes.Brid, TileTypes.Brid, TileTypes.PaLe, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Fire, TileTypes.Path, TileTypes.Fire, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Hill, TileTypes.Hill, TileTypes.Hill, TileTypes.Hill, TileTypes.Hill, TileTypes.Hill, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.EndR, TileTypes.Wall, TileTypes.Door, TileTypes.Wall, TileTypes.EndR, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.PiTG, TileTypes.Path, TileTypes.PiTG, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Gree, TileTypes.Gree, TileTypes.Roof, TileTypes.Flor, TileTypes.Flor, TileTypes.Flor, TileTypes.Roof, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.PiBG, TileTypes.Path, TileTypes.PiBG, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Gree, TileTypes.Roof, TileTypes.Flor, TileTypes.TabF, TileTypes.Flor, TileTypes.Roof, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Tree, TileTypes.Tree, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Gree, TileTypes.Roof, TileTypes.Flor, TileTypes.Flor, TileTypes.Flor, TileTypes.Roof, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.PiTG, TileTypes.Path, TileTypes.PiTG, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Tree, TileTypes.Tree, TileTypes.Path, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Path, TileTypes.Gree, TileTypes.EndW, TileTypes.Wall, TileTypes.Door, TileTypes.Wall, TileTypes.EndW, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.PiBG, TileTypes.Path, TileTypes.PiBG, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Tree, TileTypes.Tree, TileTypes.Path, TileTypes.Tree, TileTypes.Tree, TileTypes.Tree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Fire, TileTypes.Path, TileTypes.Fire, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.EndR, TileTypes.Wall, TileTypes.EndR, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.PaRi, TileTypes.Brid, TileTypes.Brid, TileTypes.PaLe, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Roof, TileTypes.Flor, TileTypes.Roof, TileTypes.ShoG, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.GrBL, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrBR, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.EndW, TileTypes.Door, TileTypes.EndW, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.GrBL, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrLe, TileTypes.Gree, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Path, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrRi, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.GrBL, TileTypes.GrBo, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.Gree, TileTypes.GrBR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.GrBL, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBo, TileTypes.GrBR, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
        { TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate, TileTypes.Wate },
    };

    public WorldMap(Bat bat)
    {
        Add(bat);
        
        this.tileTypes = new[]
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
            TileTypes.EndW,
            TileTypes.EndR,
            TileTypes.Flor,
            TileTypes.Brid,
            TileTypes.PilB,
            TileTypes.PilT,
            TileTypes.Tabl,
            TileTypes.Shri,
            TileTypes.CheO,
            TileTypes.CheC,
            TileTypes.Shop,
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
        this.bat = bat;
    }

    private TileTypes WateOverlayIndex = TileTypes.Wat1;
    private TileTypes fireIndex = TileTypes.Fir1;
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

            if (fireIndex == TileTypes.Fir1)
            {
                fireIndex = TileTypes.Fir2;
            }
            else
            {
                fireIndex = TileTypes.Fir1;
            }
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        try
        {
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
                    else if (tileType.HasFlag(TileTypes.Fire))
                    {
                        canvas.DrawImage(images[TileTypes.Gree], x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(images[fireIndex], x * tileSize, y * tileSize, tileSize, tileSize);
                    }
                    else if (tileType.HasFlag(TileTypes.Tree))
                    {
                        canvas.DrawImage(images[TileTypes.Gree], x * tileSize, y * tileSize, tileSize, tileSize);
                        canvas.DrawImage(images[TileTypes.Tree], x * tileSize, y * tileSize, tileSize, tileSize);
                    }
                    else
                    {
                        foreach (var type in this.tileTypes)
                        {
                            RenderTile(tileType, type, canvas, x, y, tileSize);
                        }    
                    }
                }
            }

            canvas.DrawImage(character, 4 * tileSize, 4 * tileSize, tileSize, tileSize);

            bat.Bounds = new RectF(11 * tileSize, 11 * tileSize, tileSize, tileSize);

            base.Render(canvas, dimensions);
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
    EndW = 131072,
    EndR = 262144,
    Flor = 524288,
    Brid = 1048576,
    Fire = 2097152,
    PilT = 4194304,
    PilB = 8388608,
    Shri = 16777216,
    Tabl = 33554432,
    CheO = 67108864,
    CheC = 134217728,
    Shop = 268435456,

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

    PaRi = Path | GrRi,
    PaLe = Path | GrLe,

    PiBG = PilB | Gree,
    PiTG = PilT | Gree,
    TabF = Tabl | Flor,
    ShrG = Shri | Gree,
    ChOG = CheO | Gree,
    ChCG = CheC | Gree,
    ShoG = Shop | Gree,
}