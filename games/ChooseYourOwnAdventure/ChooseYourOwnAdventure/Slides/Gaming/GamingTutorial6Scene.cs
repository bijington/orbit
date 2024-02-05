using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial6Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial6Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_6.png");
	}

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Client - Add Nuget Package", canvas, dimensions);

        canvas.DrawCenteredScaledImage(image, dimensions, 0.5f);

        base.Render(canvas, dimensions);
    }
}
