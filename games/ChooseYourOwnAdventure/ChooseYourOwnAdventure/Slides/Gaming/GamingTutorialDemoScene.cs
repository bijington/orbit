using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorialDemoScene : SlideSceneBase
{
	public GamingTutorialDemoScene(Pointer pointer) : base(pointer)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo Time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
