using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TipsAndTricksSimpleScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 10;

    private float shipX = -100;
    private float destinationShipX = float.NaN;

    private float asteroidX = float.NaN;
    private float destinationAsteroidX = float.NaN;

    private float pulseX = float.NaN;
    private float destinationPulseX = float.NaN;

    private readonly Microsoft.Maui.Graphics.IImage asteroid;
    private readonly Microsoft.Maui.Graphics.IImage ship;
    private readonly Microsoft.Maui.Graphics.IImage guns;
    private readonly Microsoft.Maui.Graphics.IImage pulse;
    private readonly Microsoft.Maui.Graphics.IImage collectible;

    public TipsAndTricksSimpleScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
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

        if (float.IsNaN(destinationShipX))
        {
            destinationShipX = dimensions.Width / 4;
        }

        if (float.IsNaN(asteroidX))
        {
            asteroidX = dimensions.Width * 1.2f;
        }

        if (float.IsNaN(destinationAsteroidX))
        {
            destinationAsteroidX = dimensions.Width * 0.75f;
        }

        if (currentTransition >= 1)
        {
            canvas.DrawImage(
                ship,
                shipX,
                dimensions.Center.Y - ship.Height /2,
                ship.Width,
                ship.Height);
        }

        if (currentTransition >= 2 && currentTransition < 8)
        {
            canvas.DrawImage(
                asteroid,
                asteroidX,
                dimensions.Center.Y - asteroid.Height / 2,
                asteroid.Width,
                asteroid.Height);
        }

        if (currentTransition >= 3)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4f;
            canvas.StrokeDashOffset = 4;

            canvas.DrawEllipse(
                shipX,
                dimensions.Center.Y - ship.Height / 2,
                ship.Width,
                ship.Height);

            if (currentTransition < 8)
            {
                canvas.DrawEllipse(
                    asteroidX,
                    dimensions.Center.Y - asteroid.Height / 2,
                    asteroid.Width,
                    asteroid.Height);
            }
        }

        if (currentTransition >= 4)
        {
            canvas.DrawImage(
                guns,
                (shipX + ship.Width * 0.75f) - guns.Width / 2,
                dimensions.Center.Y - guns.Height / 2,
                guns.Width,
                guns.Height);
        }

        if (currentTransition >= 5)
        {
            canvas.StrokeColor = Colors.Yellow;
            canvas.StrokeSize = 4f;

            float gunRange = 150f;

            canvas.DrawEllipse(
                shipX - gunRange,
                (dimensions.Center.Y - ship.Height / 2) - gunRange,
                ship.Width + 2 * gunRange,
                ship.Height + 2 * gunRange);

            destinationAsteroidX = (shipX - gunRange) + ship.Width + 2 * gunRange;
            destinationPulseX = destinationAsteroidX;

            if (float.IsNaN(pulseX))
            {
                pulseX = (shipX + ship.Width / 2) - guns.Width / 2;
            }
        }

        if (currentTransition == 7)
        {
            canvas.DrawImage(
                pulse,
                pulseX,
                dimensions.Center.Y - pulse.Height / 2,
                pulse.Width,
                pulse.Height);
        }

        if (currentTransition >= 8)
        {
            canvas.DrawImage(
                collectible,
                asteroidX,
                dimensions.Center.Y - collectible.Height / 2,
                collectible.Width,
                collectible.Height);
        }

        if (currentTransition >= 9)
        {
            canvas.StrokeColor = Styling.Tertiary;
            canvas.StrokeSize = 4f;

            canvas.DrawEllipse(
                asteroidX,
                dimensions.Center.Y - collectible.Height / 2,
                collectible.Width,
                collectible.Height);

            float collectionRange = 90f;

            canvas.DrawEllipse(
                shipX - collectionRange,
                (dimensions.Center.Y - ship.Height / 2) - collectionRange,
                ship.Width + 2 * collectionRange,
                ship.Height + 2 * collectionRange);
        }

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        if (currentTransition == 1)
        {
            shipX = Math.Min(shipX + 8, destinationShipX);
        }
        else if (currentTransition == 2)
        {
            asteroidX = Math.Max(asteroidX - 8, destinationAsteroidX);
        }
        else if (currentTransition == 6)
        {
            asteroidX = Math.Max(asteroidX - 8, destinationAsteroidX);
        }
        else if (currentTransition == 7)
        {
            pulseX = Math.Min(pulseX + 8, destinationPulseX);
        }
    }
}
