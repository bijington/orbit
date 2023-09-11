using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class FittingItIntoDotnetMauiScene : SlideSceneBase
{
	public FittingItIntoDotnetMauiScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
	{
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Fitting it into .NET MAUI", canvas, dimensions);

        base.Render(canvas, dimensions);
    }
}
