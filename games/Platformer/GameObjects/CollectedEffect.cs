using Orbit.Engine;

namespace Platformer.GameObjects;

public class CollectedEffect : GameObject
{
    private readonly SettingsService settingsService;
    private readonly Sprite sprite;

    public CollectedEffect(SettingsService settingsService)
    {
        this.settingsService = settingsService;
        
        sprite = new Sprite(
            images: 
            [
                LoadImage("collected_1.png"),
                LoadImage("collected_2.png"),
                LoadImage("collected_3.png"),
                LoadImage("collected_4.png"),
                LoadImage("collected_5.png"),
                LoadImage("collected_6.png")
            ],
            imageDisplayDuration: 50,
            playMode: Sprite.PlayModeType.Single);
        
        Add(sprite);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        sprite.Bounds = this.Bounds;
        
        if (this.settingsService.ShowDebug)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 1;
            canvas.DrawRectangle(this.Bounds);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (sprite.IsRunning is false)
        {
            CurrentScene.Remove(this);
        }
    }
}