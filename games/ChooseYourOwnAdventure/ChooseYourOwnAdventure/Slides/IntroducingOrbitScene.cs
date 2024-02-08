using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class IntroducingOrbitScene : SlideSceneBase
{

    public IntroducingOrbitScene(Pointer pointer) : base(pointer)
    {
    }

    public override string Notes =>
"""
We've covered the what and why, now I would like to introduce you all to some of the how.

I have built a library called Orbit, named after the first game I attempted to build with it.

It is a lightweight 2D game engine. - possible to create fake 3D through the use of shadows, etc.

Build on top of .NET MAUI Graphics

Which provides some simple implementations for rendering our game.

And there are 2 key components to the engine these are:

a Game Scene which can be used to represent a level or in todays case a slide

and there are Game Objects which can be used to represent anything that we want in our scene.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("The Orbit engine", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Lightweight 2D game engine

- Built on top of .NET MAUI Graphics

- Simple implementations for rendering

- 2 Key components:

  - GameScene - this represents a level

  - GameObject - this represents a character, etc.
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
