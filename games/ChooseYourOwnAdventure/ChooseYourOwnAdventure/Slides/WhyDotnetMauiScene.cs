using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class WhyDotnetMauiScene : SlideSceneBase
{
    public WhyDotnetMauiScene(Pointer pointer) : base(pointer)
    {
    }

    public override string Notes => 
"""
Some people have asked me "why use .NET MAUI for building games?".

Now I believe it is a really good candidate for building games.

My journey so far has enabled me to stay in my safe, happy place - the .NET ecosystem while still trying to push the boundaries of my own knowledge.

- Unified Graphics API for each platform

- .NET is fast!

- Mature ecosystem

- Cross-platform
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Why use .NET MAUI?", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- .NET is my happy place

- Unified Graphics API for each platform

- .NET is fast!

- Mature ecosystem

- Cross-platform
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
