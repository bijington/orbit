using Orbit.Engine;

namespace Platformer.GameObjects;

public class Cherries : GameObject
{
    private readonly SettingsService settingsService;
    private readonly float x;
    private readonly Sprite sprite;

    public Cherries(SettingsService settingsService, float x)
    {
        this.settingsService = settingsService;
        this.x = x;

        sprite = new Sprite(
            images: 
            [
                LoadImage("cherries_1.png"),
                LoadImage("cherries_2.png"),
                LoadImage("cherries_3.png"),
                LoadImage("cherries_4.png"),
                LoadImage("cherries_5.png"),
                LoadImage("cherries_6.png"),
                LoadImage("cherries_7.png"),
                LoadImage("cherries_8.png"),
                LoadImage("cherries_9.png"),
                LoadImage("cherries_10.png"),
                LoadImage("cherries_11.png"),
                LoadImage("cherries_12.png"),
                LoadImage("cherries_13.png"),
                LoadImage("cherries_14.png"),
                LoadImage("cherries_15.png"),
                LoadImage("cherries_16.png"),
                LoadImage("cherries_17.png")
            ],
            imageDisplayDuration: 50);
        
        Add(sprite);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        // Need to clean this up.
        this.Bounds = new RectF(this.x * dimensions.Width, dimensions.Height - 128, 64, 64);
        
        sprite.Bounds = this.Bounds;

        if (this.settingsService.ShowDebug)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 1;
            canvas.DrawRectangle(this.Bounds);
        }

        base.Render(canvas, dimensions);
    }

    internal void Collide()
    {
        CurrentScene.Remove(this);
        CurrentScene.Add(new CollectedEffect(this.settingsService) { Bounds = this.Bounds });
    }
}