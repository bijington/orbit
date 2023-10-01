using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class DotnetMauiGraphicsScene : SlideSceneBase
{
	public DotnetMauiGraphicsScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle(".NET MAUI Graphics", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Unified graphics API

- Provides surface to draw or paint on

- Platform specific implementations

  - Android -> Android.Graphics

  - iOS/macOS -> CoreGraphics

  - Windows -> Microsoft.Graphics/Windows.Graphics

- GraphicsView",
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
