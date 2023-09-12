using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TipsAndTricksSimpleScene : SlideSceneBase
{
	public TipsAndTricksSimpleScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Keep it simple", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"- Cross-platform application framework

  - Mobile: Android and iOS

  - Desktop: macOS and Windows

  - Smart things: Tizen - Samsung devices

- Evolution of Xamarin.Forms

  - Version 8.0 coming this November

  - Finally a first class citizen of the .NET ecosystem

  - More familiar to other .NET code bases",
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
