using Orbit.Engine;

using IImage = Microsoft.Maui.Graphics.IImage;

namespace Platformer;

public class FloorTile : GameObject
{
    private readonly IImage image;

    public FloorTile()
    {
        image = LoadImage("floor.png");

        this.Bounds = new RectF(0, 0, 64, 64);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        const float tileWidth = 64;
        
        var columns = Math.Ceiling(dimensions.Width / tileWidth);
        
        for (int i = 0; i < columns; i++)
        {
            canvas.DrawImage(image, i * tileWidth, dimensions.Height - tileWidth, tileWidth, tileWidth);   
        }

        base.Render(canvas, dimensions);
    }
}