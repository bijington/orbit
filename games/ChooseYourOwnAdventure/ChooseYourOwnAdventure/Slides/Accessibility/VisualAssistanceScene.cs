using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Accessibility;

public class VisualAssistanceScene : SlideSceneBase
{
	public VisualAssistanceScene(Pointer pointer) : base(pointer)
    {
	}

    public override string Notes => 
"""
Web Content Accessibility Guidelines

There are some fantastic resources out there that help to understand how we can make our apps more accessible.

Both WCAG and Microsoft highlight some key details but a few quick tips are to consider things like

- Make sure your UI responds to system font scaling

- Use colours that have a good contrast ratio (16:1)

- Provide multiple modes of interaction
    
- Useful links
https://www.w3.org/WAI/fundamentals/accessibility-principles/

https://learn.microsoft.com/dotnet/maui/fundamentals/accessibility
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("WCAG Accessibility Principles", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Make sure your UI responds to system font scaling

- Use colours that have a good contrast ratio (16:1)

- Provide multiple modes of interaction
    
- Useful links
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
