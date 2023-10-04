using Orbit.Engine;

namespace Orbit.GameObjects;

public class Ship : GameObject
{
    readonly Microsoft.Maui.Graphics.IImage image;
    private readonly Thruster thruster;
    private readonly SettingsManager settingsManager;
    private readonly float aspectRatio;
    public float angle = 0f;

    // TODO: Different types of collision here:
    // 1. Collide with Asteroid - damages ship
    // 2. Move into shadow - stop regenerating power

    public override bool IsCollisionDetectionEnabled => true;

    public Ship(
        Thruster thruster,
        Gun gun,
        Battery battery,
        SettingsManager settingsManager)
    {
        image = LoadImage("ship.png");

        aspectRatio = image.Width / image.Height;

        this.thruster = thruster;
        this.settingsManager = settingsManager;
        Add(gun);
        Add(thruster);
        Add(battery);

        gun.Ship = this;
        thruster.Ship = this;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        var orbitRadius = Math.Min(dimensions.Width, dimensions.Height) / 4;

        var theta = angle * MathF.PI / 180;
        var adjacent = MathF.Cos(theta) * orbitRadius;
        var opposite = MathF.Sin(theta) * orbitRadius;

        var size = Math.Min(dimensions.Width, dimensions.Height) / 10;

        var imageWidth = size;
        var imageHeight = size / aspectRatio;

        var halfWidth = imageWidth / 2;
        var halfHeight = imageHeight / 2;

        Bounds = new RectF(
            dimensions.Center.X + adjacent - halfWidth,
            dimensions.Center.Y + opposite - halfHeight,
            imageWidth,
            imageHeight);

        canvas.Translate(Bounds.X + halfWidth, Bounds.Y + halfHeight);
        canvas.Rotate(angle + 90);

        canvas.DrawImage(image, -halfWidth, -halfHeight, Bounds.Width, Bounds.Height);

        if (settingsManager.ShowDebug)
        {
            canvas.StrokeColor = Colors.OrangeRed;
            canvas.StrokeSize = 4;
            canvas.DrawEllipse(-halfWidth, -halfHeight, Bounds.Width, Bounds.Height);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        angle = (angle + this.thruster.Thrust) % 360;
    }
}
