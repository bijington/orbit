using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class DemoTimePoliticiansScene : SlideSceneBase, IDestinationKnowingScene
{
	public DemoTimePoliticiansScene(Pointer pointer) : base(pointer)
    {
	}

    public Type DestinationSceneType => typeof(Stage1SummaryScene);

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
