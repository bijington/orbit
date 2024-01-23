using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Tutorials;

public class TutorialPartThreeScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

    public override string Notes => 
        """
        s
        """;

    public TutorialPartThreeScene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("signalr.png");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Tutorial - What is a BackgroundService?", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            """
            - Run background tasks

            - Can be hosted in many ways

            - Event or timer based

            - Scalable
            """,
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        canvas.DrawImage(
            image,
            dimensions.Width * 0.4f,
            dimensions.Height / 4,
            image.Width * 2.5f,
            image.Height * 2.5f);

        base.Render(canvas, dimensions);
    }
}
