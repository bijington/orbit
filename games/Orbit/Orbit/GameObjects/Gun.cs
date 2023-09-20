using Orbit.Engine;

namespace Orbit.GameObjects;

public class Gun : GameObject
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly IServiceProvider serviceProvider;
    readonly Microsoft.Maui.Graphics.IImage image;

    public Ship Ship { get; set; }

    public Gun(IGameSceneManager gameSceneManager, IServiceProvider serviceProvider)
    {
        this.gameSceneManager = gameSceneManager;
        this.serviceProvider = serviceProvider;

        image = LoadImage("gun.png");
    }

    public override bool IsCollisionDetectionEnabled => true;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Translate(dimensions.Center.X, dimensions.Center.Y);
        canvas.DrawImage(image, 0, 0, image.Width, image.Height);
        canvas.Rotate(Ship.angle);

        //canvas.StrokeColor = Colors.OrangeRed;
        //canvas.StrokeSize = 4;
        //canvas.StrokeDashPattern = new float[] { 4, 4 };
        //canvas.DrawEllipse(175, -125, 300, 300);
    }

    private bool hasFired;

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (!hasFired)
        {
            hasFired = true;

            Pulse pulse = serviceProvider.GetRequiredService<Pulse>();

            pulse.SetMovement(new Movement(new PointF(0.6f, 0.6f), new Point(20, 20), new Point(0, 0)));

            CurrentScene.Add(pulse);
        }
    }
}
