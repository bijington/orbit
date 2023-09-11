using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TheGameEngineApproachScene : SlideSceneBase
{
	public TheGameEngineApproachScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
	{
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("The game engine approach", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
