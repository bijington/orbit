using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TipsAndTricksSimplePartTwoScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 4;

    private readonly Microsoft.Maui.Graphics.IImage asteroid;
    private readonly Microsoft.Maui.Graphics.IImage ship;
    private readonly Microsoft.Maui.Graphics.IImage guns;
    private readonly Microsoft.Maui.Graphics.IImage pulse;
    private readonly Microsoft.Maui.Graphics.IImage collectible;

    public TipsAndTricksSimplePartTwoScene(Pointer pointer) : base(pointer)
    {
        ship = LoadImage("ship_basic.png");
        guns = LoadImage("guns.png");
        asteroid = LoadImage("asteroid.png");
        pulse = LoadImage("pulse.png");
        collectible = LoadImage("collectible.png");
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
        Styling.RenderTitle("Keep it simple", canvas, dimensions);

        if (currentTransition >= 1)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4f;
            canvas.StrokeDashPattern = new[] { 4f, 4f };
            canvas.StrokeDashOffset = 4;

            var shipX = dimensions.Width * 0.15f;
            var y = dimensions.Height / 4;

            canvas.DrawImage(
                ship,
                shipX,
                y - ship.Height / 2,
                ship.Width,
                ship.Height);

            canvas.DrawEllipse(
                shipX,
                y - ship.Height / 2,
                ship.Width,
                ship.Height);

            var asteroidX = shipX + ship.Width + asteroid.Width;

            canvas.DrawImage(
                asteroid,
                asteroidX,
                y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);

            canvas.DrawEllipse(
                asteroidX,
                y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);
        }

        if (currentTransition >= 2)
        {
            canvas.StrokeColor = Colors.Yellow;
            canvas.StrokeSize = 4f;
            canvas.StrokeDashOffset = 4;

            var x = dimensions.Width * 0.6f;
            var y = dimensions.Height / 4;

            canvas.DrawImage(
                guns,
                x,
                y - guns.Height / 2,
                guns.Width,
                guns.Height);

            float gunRange = 150f;

            canvas.DrawEllipse(
                x - gunRange,
                (y - guns.Height / 2) - gunRange,
                guns.Width + 2 * gunRange,
                guns.Height + 2 * gunRange);

            var asteroidX = x + ship.Width + asteroid.Width;

            canvas.DrawImage(
                asteroid,
                asteroidX,
                y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);

            canvas.DrawEllipse(
                asteroidX,
                y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);
        }

        if (currentTransition >= 3)
        {
            canvas.StrokeColor = Styling.CodeColor;
            canvas.StrokeSize = 4f;
            canvas.StrokeDashOffset = 4;

            var x = dimensions.Width * 0.2f;
            var y = dimensions.Height * 0.75f;

            canvas.DrawImage(
                pulse,
                x,
                y - pulse.Height / 2,
                pulse.Width,
                pulse.Height);

            canvas.DrawEllipse(
                x,
                y - pulse.Height / 2,
                pulse.Width,
                pulse.Height);

            var asteroidX = x + pulse.Width + asteroid.Width;

            canvas.DrawImage(
                asteroid,
                asteroidX,
                y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);

            canvas.DrawEllipse(
                asteroidX,
                y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);
        }

        if (currentTransition >= 4)
        {
            canvas.StrokeColor = Styling.Tertiary;
            canvas.StrokeSize = 4f;
            canvas.StrokeDashOffset = 4;

            var x = dimensions.Width * 0.6f;
            var y = dimensions.Height * 0.75f;

            float collectionRange = 90f;

            canvas.DrawEllipse(
                x - collectionRange,
                (y - ship.Height / 2) - collectionRange,
                ship.Width + 2 * collectionRange,
                ship.Height + 2 * collectionRange);

            var collectibleX = x + ship.Width + collectionRange + collectible.Width;

            canvas.DrawImage(
                collectible,
                collectibleX,
                y - collectible.Height / 2,
                collectible.Width,
                collectible.Height);

            canvas.DrawEllipse(
                collectibleX,
                y - collectible.Height / 2,
                collectible.Width,
                collectible.Height);
        }

        base.Render(canvas, dimensions);
    }
}
