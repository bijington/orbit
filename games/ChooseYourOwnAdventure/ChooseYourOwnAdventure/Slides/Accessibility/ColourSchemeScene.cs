using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class ColourSchemeScene : SlideSceneBase
{
	public ColourSchemeScene(Pointer pointer) : base(pointer)
    {
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Demo time", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
