using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class VisualAssistanceScene : SlideSceneBase
{
	public VisualAssistanceScene(Pointer pointer) : base(pointer)
    {
	}

    public override string Notes => 
"""
- Perceivable information and user interface
    - Text alternatives are provided for non-text content
    - Captions are provided for audio and video content
    - Content is adaptable to different presentation formats
    - Content is distinguishable from its background

We've covered the concept of using SemanticProperties to aid the screen reader.
Other things to consider is enabling your users to change the font size, or the colour scheme of your app.

- Operable user interface and navigation
    - Functionality is available from a keyboard
    - Users have enough time to read and use the content
    - Content does not cause seizures and physical reactions
    - Users can easily navigate, find content, and determine where they are
    - Users can use different input modalities beyond keyboard

We mentioned earlier on that .NET MAUI provides many possibilities to interact with an application.

- Understandable information and user interface
    - Text is readable and understandable
    - Content appears and operates in predictable ways
    - Users are helped to avoid and correct mistakes

An example of this is to present the pucks trajectory to the user, so they can see where it is going to go.

- Robust content and reliable interpretation
    - Content is compatible with current and future user tools


""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("WCAG Accessibility Principles", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Perceivable information and user interface

- Operable user interface and navigation

- Understandable information and user interface

- Robust content and reliable interpretation
    
https://www.w3.org/WAI/fundamentals/accessibility-principles/

https://learn.microsoft.com/dotnet/maui/fundamentals/accessibility
""",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }
}
