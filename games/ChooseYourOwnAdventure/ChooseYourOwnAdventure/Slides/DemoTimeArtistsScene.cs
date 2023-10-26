using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class DemoTimeArtistsScene : SlideSceneBase, IDestinationKnowingScene
{
	public DemoTimeArtistsScene(Pointer pointer) : base(pointer)
    {
	}

    public Type DestinationSceneType => typeof(Stage1SummaryScene);

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
