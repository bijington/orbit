using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial7Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage[] images;
    private int currentTransition = 0;
    private const int transitions = 2;

	public GamingTutorial7Scene(Pointer pointer) : base(pointer)
    {
        images = [LoadImage("gaming_tutorial_7_0.png"), LoadImage("gaming_tutorial_7_1.png"), LoadImage("gaming_tutorial_7_2.png")];
	}

    public override void Progress()
    {
        // If we are complete then fire the Next event.
        if (currentTransition == transitions)
        {
            base.Progress();
        }

        currentTransition++;
    }

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Client - Build a connection", canvas, dimensions);

        var image = images[currentTransition];

        canvas.DrawCenteredScaledImage(image, dimensions, 0.5f);

        base.Render(canvas, dimensions);
    }
}
