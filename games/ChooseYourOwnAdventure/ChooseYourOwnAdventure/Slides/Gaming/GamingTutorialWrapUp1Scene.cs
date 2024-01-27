using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorialWrapUp1Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorialWrapUp1Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_wrapup_1.jpg");
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Considerations - travel light", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Only send what you need

- Pay close attention to the data types you send

- Consider using MessagePack*

* MessagePack requires some extra work when using in an AOT environment.
https://learn.microsoft.com/aspnet/core/signalr/messagepackhubprotocol
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

        canvas.DrawImage(image, dimensions.Right - imageWidth - 80, dimensions.Bottom - imageHeight - 80, imageWidth, imageHeight);

        base.Render(canvas, dimensions);
    }
}
