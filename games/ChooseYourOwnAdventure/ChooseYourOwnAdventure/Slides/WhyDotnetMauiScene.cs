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

Essentially .NET MAUI Graphics offers us with a surface that can render pixel perfect graphics on any platform supported by .NET MAUI. We should consider .NET MAUI Graphics as an abstraction layer, like .NET MAUI itself, on top of the platform specific drawing libraries. So we get all the power of each platform but with a simple unified .NET API that we as developers can work with.

By delegating out to the platform specific drawing libraries we can obtain great performance when rendering our games because the platform specific libraries (with the current exception of Windows) provides the ability to utilise hardware acceleration for us.

We do also have the ability to switch out the .NET MAUI implementation for graphics rendering and replace with SkiaSharp or even a custom implementation if we so desire.

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

- Multiple Graphics options

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
