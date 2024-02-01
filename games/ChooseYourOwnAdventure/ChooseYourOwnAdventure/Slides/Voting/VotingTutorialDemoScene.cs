using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Voting;

public class VotingTutorialDemoScene : SlideSceneBase
{
	public VotingTutorialDemoScene(Pointer pointer) : base(pointer)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo Time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
