using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class WhatsOurProgressScene : SlideSceneBase
{
	public WhatsOurProgressScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
	{
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("What's our progress?", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
