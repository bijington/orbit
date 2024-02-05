using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial3Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial3Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_3.png");
	}

    public override string Notes => 
"""
Let's take a look at each component and how we integrate it into our solution.

First up is the Hub.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Registering the hub", canvas, dimensions);

        canvas.DrawCenteredScaledImage(image, dimensions, 0.5f);

        base.Render(canvas, dimensions);
    }
}
