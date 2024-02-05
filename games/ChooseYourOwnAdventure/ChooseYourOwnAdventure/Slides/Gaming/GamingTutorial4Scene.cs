using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial4Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial4Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_4.png");
	}

    public override string Notes => 
"""
We mentioned that a Background Service is great for performing background tasks.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Creating a Background Service", canvas, dimensions);

        canvas.DrawCenteredScaledImage(image, dimensions, 0.5f);

        base.Render(canvas, dimensions);
    }
}
