using Orbit.Engine;

namespace BuildingGames.GameObjects;

public class Achievement : GameObject
{
    private double duration;
    private double threshold = 60 * 1000 * 1000; // 1 hour

    private double y = -100;

    private BannerMode bannerMode;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        double bannerWidth = 600;
        double xOffset = 300;
        double x = dimensions.Center.X - xOffset;

        if (bannerMode is not BannerMode.None or BannerMode.Hidden)
        {
            canvas.FillColor = Styling.Tertiary;
            canvas.FillRoundedRectangle(new Rect(x, y, bannerWidth, 100), 10d);

            canvas.Font = Styling.Font;
            canvas.FontColor = Styling.TitleColor;

            canvas.DrawString(
                "Next speaker unlocked",
                new Rect(x, y, bannerWidth, 50),
                HorizontalAlignment.Center,
                VerticalAlignment.Center);

            canvas.DrawString(
                "Listened to me drone on for 60 minutes!",
                new Rect(x, y + 50, bannerWidth, 50),
                HorizontalAlignment.Center,
                VerticalAlignment.Center);
        }
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        switch (bannerMode)
        {
            case BannerMode.None:
                duration += millisecondsSinceLastUpdate;

                if (duration >= threshold)
                {
                    bannerMode = BannerMode.Displaying;
                }
                break;

            case BannerMode.Displaying:
                y += millisecondsSinceLastUpdate;

                if (y >= 10)
                {
                    bannerMode = BannerMode.Display;
                    duration = 0;
                    threshold = 10_000; // 10s
                }
                break;

            case BannerMode.Display:
                duration += millisecondsSinceLastUpdate;

                if (duration >= threshold)
                {
                    bannerMode = BannerMode.Hiding;
                }
                break;

            case BannerMode.Hiding:
                y -= millisecondsSinceLastUpdate;

                if (y <= -100)
                {
                    bannerMode = BannerMode.Hidden;
                    duration = 0;
                }
                break;

            case BannerMode.Hidden:
                break;

            default:
                break;
        }
    }
}

internal enum BannerMode
{
    None,
    Displaying,
    Display,
    Hiding,
    Hidden
}
