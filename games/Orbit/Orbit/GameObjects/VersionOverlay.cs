using Orbit.Engine;

namespace Orbit.GameObjects;

public class VersionOverlay : GameObject
{
    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        canvas.Font = Microsoft.Maui.Graphics.Font.Default;
        canvas.FontColor = Colors.White;
        canvas.DrawString(
            GitVersionInformation.FullSemVer,
            dimensions.X,
            dimensions.Y,
            dimensions.Width - 10,
            dimensions.Height - 10,
            HorizontalAlignment.Right,
            VerticalAlignment.Bottom);
    }
}
