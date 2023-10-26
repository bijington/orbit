using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class GameDemoScene : SlideSceneBase, IDestinationKnowingScene
{
	public GameDemoScene(Pointer pointer) : base(pointer)
    {
	}

    public Type DestinationSceneType => typeof(TipsAndTricksSimpleScene);

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Finally game time", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            @"
Demo time!",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.05),
            new PointF(40, dimensions.Height * 0.2f),
            HorizontalAlignment.Center,
            VerticalAlignment.Center);

        base.Render(canvas, dimensions);
    }
}
