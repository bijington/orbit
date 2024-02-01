using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class NonVisualFeedbackScene : SlideSceneBase, IDestinationKnowingScene
{
    private readonly Microsoft.Maui.Graphics.IImage image;
	public NonVisualFeedbackScene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("non_visual_feedback.png");
	}

    public override string Notes => 
"""
- Haptic feedback

- Vibration feedback

- Audio feedback

    - Sound effects

    - Background music

    - Screen reader
""";

    public Type DestinationSceneType => typeof(TheYearOfAIScene);

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Non visual feedback", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Haptic feedback

- Vibration feedback

- Audio feedback

    - Sound effects

    - Background music

    - Screen reader
""",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Right - imageWidth - 80, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);
    }
}
