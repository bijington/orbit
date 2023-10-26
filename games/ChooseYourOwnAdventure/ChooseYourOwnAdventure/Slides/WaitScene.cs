using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class WaitScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage wait;

    public WaitScene(Pointer pointer) : base(pointer)
    {
        wait = LoadImage("wait.png");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Wait", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Time to chill",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        canvas.DrawImage(wait, dimensions.Center.X - wait.Width / 2, dimensions.Center.Y - wait.Height / 2, wait.Width, wait.Height);

        base.Render(canvas, dimensions);
    }
}
