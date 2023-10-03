using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class DemoTimePoliticiansScene : SlideSceneBase, IDestinationKnowingScene
{
	public DemoTimePoliticiansScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
	}

    public Type DestinationSceneType => typeof(MagicianScene);

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
