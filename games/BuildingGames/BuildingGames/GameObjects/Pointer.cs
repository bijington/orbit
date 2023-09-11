using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class Pointer : GameObject
{
    private readonly ControllerManager controllerManager;
    private bool keyPressHandled;

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
            canvas.FillCircle(this.controllerManager.PointerLocation, this.controllerManager.PointerRadius);
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

            if (this.controllerManager.CurrentPressedButton == ControllerButton.NavigateForward)
            {
                if (this.keyPressHandled is false)
                {
                    this.controllerManager.PointerRadius *= 2;
                }
                this.keyPressHandled = true;
            }
            else if (this.controllerManager.CurrentPressedButton == ControllerButton.NavigateBackward)
            {
                if (this.keyPressHandled is false)
                {
                    this.controllerManager.PointerRadius /= 2;
                }
                this.keyPressHandled = true;
            }
            else
            {
                this.keyPressHandled = false;
            }
        }
    }
}
