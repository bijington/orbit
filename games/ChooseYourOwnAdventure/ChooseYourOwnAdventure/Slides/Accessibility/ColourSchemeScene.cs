using BuildingGames.GameObjects;
using ChooseYourOwnAdventure.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class ColourSchemeScene : SlideSceneBase
{
	public ColourSchemeScene(Pointer pointer) : base(pointer)
    {
        Character.Position = Character.Positions.Accessibility1;
        Character.Position = Character.Positions.Accessibility2;
        Character.Position = Character.Positions.Accessibility3;
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Colours", canvas, dimensions);

        // Render the splotch test images to highlight a lack of color depth.
        // Render some patterns to show how to avoid the issue.

        base.Render(canvas, dimensions);
    }
}
