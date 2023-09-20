using Orbit.Engine;

namespace Orbit.GameObjects;

public class Planet : GameObject
{
    Microsoft.Maui.Graphics.IImage image;
    float angle = 0;
    const float rotationIncrement = -0.25f;
    private readonly IGameSceneManager gameSceneManager;
    private readonly IVibration vibration;

    public float HealthPoints { get; private set; } = 100;

    public float MaxHealthPoints { get; private set; } = 100;

    public Planet(
        IGameSceneManager gameSceneManager,
        Shadow shadow,
        IVibration vibration)
    {
        image = LoadImage("planet.png");
        this.gameSceneManager = gameSceneManager;
        this.vibration = vibration;
        Add(shadow);

        shadow.Planet = this;
    }

    public override bool IsCollisionDetectionEnabled => true;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);
        
        canvas.Rotate(angle, dimensions.Center.X, dimensions.Center.Y);

        var size = Math.Min(dimensions.Width, dimensions.Height) / 6;

        Bounds = new RectF(
            dimensions.Center.X - size,
            dimensions.Center.Y - size,
            size * 2,
            size * 2);

        canvas.DrawImage(image, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

        if (MainPage.ShowBounds)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        angle += rotationIncrement;
    }

    public void OnHit(int damage)
    {
        HealthPoints = Math.Max(HealthPoints - damage, 0);

        this.vibration.Vibrate();

        if (HealthPoints == 0)
        {
            gameSceneManager.GameOver();
        }
    }
}
