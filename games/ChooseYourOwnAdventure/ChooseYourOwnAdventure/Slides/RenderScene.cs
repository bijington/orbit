using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class RenderScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 3;
    private float asteroidX = float.NaN;

    private readonly Microsoft.Maui.Graphics.IImage paint;
    private readonly Microsoft.Maui.Graphics.IImage asteroid;
    private readonly Microsoft.Maui.Graphics.IImage ship;

    private readonly IList<string> textTransitions;

    public override string Notes =>
"""
The final stop in our loop is to render the game.

We will be utilising that unified graphics API that we spoke about earlier. Which provides us with a GraphicsView and canvas to draw on.

This is where we will be able to draw the player and the enemies on the screen.

The interaction with the graphics API is stack based. 

So to simplify the render of a rotating asteroid gravitating towards our ship we can:

- Translate to the centre of the asteroid

- Rotate the canvas

- and draw. This allows us to build up our scene in a very simple way.
""";

    public RenderScene(Pointer pointer) : base(pointer)
    {
        paint = LoadImage("paint.png");
        asteroid = LoadImage("asteroid.png");
        ship = LoadImage("ship_basic.png");

        textTransitions = new List<string>
        {
            @"- .NET MAUI Graphics

- Unified graphics API",

            @"- .NET MAUI Graphics

- Unified graphics API

- Provides surface to draw or paint on",

            @"- .NET MAUI Graphics

- Unified graphics API

- Provides surface to draw or paint on

- Translate game state into visuals",

            @"- .NET MAUI Graphics

- Unified graphics API

- Provides surface to draw or paint on

- Translate game state into visuals

- Stack based interaction

  - Translate

  - Rotate

  - Draw"
        };
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

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        Styling.RenderTitle("Render", canvas, dimensions);

        canvas.DrawString(
            dimensions,
            textTransitions[currentTransition],
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var y = dimensions.Height * 0.18f;
        var availableVerticalSpace = dimensions.Height - 2 * y;
        var padding = (availableVerticalSpace - (paint.Height * 0.8f * 4)) / 3;

        canvas.ClipRectangle(dimensions.Center.X + 40, y, dimensions.Width / 2 - 80, availableVerticalSpace);

        if (currentTransition != 3)
        {
            asteroidX = dimensions.Width;
        }

        if (currentTransition >= 1)
        {
            canvas.StrokeSize = 4f;
            canvas.StrokeColor = Styling.Secondary;
            canvas.StrokeDashPattern = new[] { 10f, 10f, 10f, 10f };
            canvas.DrawRectangle(dimensions.Center.X + 40, y, dimensions.Width / 2 - 80, availableVerticalSpace);

            if (currentTransition == 1)
            {
                canvas.DrawImage(paint, dimensions.Width * 0.75f, dimensions.Center.Y - (paint.Height * 0.8f), paint.Width * 0.8f, paint.Height * 0.8f);
            }
        }

        if (currentTransition > 1)
        {
            canvas.DrawImage(ship, dimensions.Width * 0.6f, dimensions.Center.Y - (ship.Height * 0.8f), ship.Width * 0.8f, ship.Height * 0.8f);
        }

        if (currentTransition == 3)
        {
            canvas.Translate(asteroidX, dimensions.Center.Y - (asteroid.Height * 0.8f));
            canvas.Rotate(angle);
            canvas.DrawImage(asteroid, -(asteroid.Width * 0.8f) / 2, -(asteroid.Height * 0.8f) / 2, asteroid.Width * 0.8f, asteroid.Height * 0.8f);

            canvas.DrawRectangle(-(asteroid.Width * 0.8f) / 2, -(asteroid.Height * 0.8f) / 2, asteroid.Width * 0.8f, asteroid.Height * 0.8f);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (currentTransition == 3)
        {
            asteroidX -= 0.2f;

            angle -= 0.1f;
        }
    }

    private float angle = 0f;
}
