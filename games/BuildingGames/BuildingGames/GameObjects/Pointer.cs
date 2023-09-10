using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class Pointer : GameObject
{
    private readonly ControllerManager controllerManager;

    public Pointer(ControllerManager controllerManager)
    {
        this.controllerManager = controllerManager;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        if (this.controllerManager.Mode == ControlMode.Pointer)
        {
            canvas.FillColor = Colors.Orange;
            canvas.FillCircle(this.controllerManager.PointerLocation, 4);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (this.controllerManager.Mode == ControlMode.Pointer)
        {
            this.controllerManager.PointerLocation = new(
                this.controllerManager.PointerLocation.X + this.controllerManager.DirectionalChange.X * (float)millisecondsSinceLastUpdate,
                this.controllerManager.PointerLocation.Y + this.controllerManager.DirectionalChange.Y * (float)millisecondsSinceLastUpdate);
        }
    }
}
