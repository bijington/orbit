using Orbit.Engine;

using Platformer.GameObjects;

using IImage = Microsoft.Maui.Graphics.IImage;

namespace Platformer.GameScenes;

public class FirstScene : GameScene
{
    private readonly SettingsService settingsService;
    private readonly IImage tile;
    
    public FirstScene(
        PinkMan pinkMan,
        FloorTile floorTile,
        SettingsService settingsService)
    {
        this.settingsService = settingsService;
        
        Add(pinkMan);
        Add(floorTile);
        
        for (int i = 2; i < 12; i++)
        {
            var cherries = new Cherries(this.settingsService, i * 0.08f);
            
            Add(cherries);   
        }
        
        tile = LoadImage("purple.png");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        var columns = dimensions.Width / 64;
        var rows = dimensions.Height / 64;
        
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                canvas.DrawImage(tile, x * 64, y * 64, 64, 64);
            }   
        }

        if (this.settingsService.ShowDebug)
        {
            canvas.FontSize = 50;
            canvas.FontColor = Colors.Red;
            canvas.DrawString(
                GameObjectsSnapshot.Count.ToString(),
                dimensions,
                HorizontalAlignment.Right,
                VerticalAlignment.Top);   
        }
        
        base.Render(canvas, dimensions);
    }
}
