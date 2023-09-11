using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TheFinalBossScene : SlideSceneBase
{
	public TheFinalBossScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
	{
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("The final boss", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
