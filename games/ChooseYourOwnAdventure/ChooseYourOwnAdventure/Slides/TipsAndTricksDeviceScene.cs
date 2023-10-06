using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TipsAndTricksDeviceScene : SlideSceneBase
{
	public TipsAndTricksDeviceScene(Pointer pointer) : base(pointer)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Consider the device/player", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Update wants to be display independent

  - Screen sizes

  - Screen orientations

- Touch offers very little feedback

  - Haptic/Vibration goes a long way

- Audio makes a huge difference",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }
}
