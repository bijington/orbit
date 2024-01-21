using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorialDemoScene : SlideSceneBase, IDestinationKnowingScene
{
	public GamingTutorialDemoScene(Pointer pointer) : base(pointer)
    {
	}

    public Type DestinationSceneType => typeof(Stage1SummaryScene);

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo Time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
