using Orbit.Engine;

namespace Orbit.GameObjects;

public class Gun : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly IServiceProvider serviceProvider;
    readonly Microsoft.Maui.Graphics.IImage image;
    private double firingSpeed = 800;
    private double elapsed = 0;

    public Ship Ship { get; set; }

    public Gun(IGameSceneManager gameSceneManager, IServiceProvider serviceProvider)
    {
        this.gameSceneManager = gameSceneManager;
        this.serviceProvider = serviceProvider;

        image = LoadImage("side_guns.png");
    }

    public override bool IsCollisionDetectionEnabled => true;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        //canvas.DrawImage(image, 0, 0, image.Width, image.Height);


        if (elapsed >= firingSpeed)
        {
            elapsed = 0;

            Pulse pulse = serviceProvider.GetRequiredService<Pulse>();

            var a = Ship.angle;

            pulse.SetMovement(
                new Movement(
                    new PointF(
                        0, 0),
                    new Point(2, 2),
                    new Point(0.01, 0.01)),
                Ship.angle);

            CurrentScene.Add(pulse);
        }

        //canvas.DrawImage(image, orbitRadius, 0, image.Width, image.Height);

        //canvas.StrokeColor = Colors.OrangeRed;
        //canvas.StrokeSize = 4;
        //canvas.StrokeDashPattern = new float[] { 4, 4 };
        //canvas.DrawEllipse(175, -125, 300, 300);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        elapsed += millisecondsSinceLastUpdate;
    }
}
