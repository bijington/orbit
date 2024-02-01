using BuildingGames.GameObjects;
using ChooseYourOwnAdventure.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class ColourSchemeScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage[] images;

    private int currentTransition = 0;
    private readonly int transitions;

	public ColourSchemeScene(Pointer pointer) : base(pointer)
    {
        Character.Position = Character.Positions.Accessibility1;
        Character.Position = Character.Positions.Accessibility2;
        Character.Position = Character.Positions.Accessibility3;

        images = [LoadImage("color_blindness_test_chart.jpg"), LoadImage("color_spectrum.jpg"), LoadImage("color_tiles.png")];

        transitions = images.Length;
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
"""
I am actually red green colour blind and have a tough time distinguishing certain colours. Does anyone here have difficulty seeing the supposed numbers in these images?

This shows how some people will perceive colours. In order to build a more accessible app or game we can look to finding a scheme that fits. 

I recommend considering introducing patterns so stripes or other shapes that can help to aid users identify specific things.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Colour schemes", canvas, dimensions);

        var image = images[currentTransition];

        canvas.DrawImage(image, dimensions.Center.X - image.Width / 2, dimensions.Center.Y - image.Height / 2, image.Width, image.Height);

        base.Render(canvas, dimensions);
    }
}
