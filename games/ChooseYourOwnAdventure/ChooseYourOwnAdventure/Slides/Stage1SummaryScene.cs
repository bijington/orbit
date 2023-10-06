using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class Stage1SummaryScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 5;

    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly IList<string> textTransitions;

    public Stage1SummaryScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
        image = LoadImage("orbit_sketch.jpg");

        textTransitions = new List<string>
        {
            @"What have we accomplished?",

            @"What have we accomplished?

- Learned how to use SignalR",

            @"What have we accomplished?

- Learned how to use SignalR

- Stayed within our comfort zone",

            @"What have we accomplished?

- Learned how to use SignalR

- Stayed within our comfort zone

What's next?",

            @"What have we accomplished?

- Learned how to use SignalR

- Stayed within our comfort zone

What's next?

- Show you the big bang idea",

            @"What have we accomplished?

- Learned how to use SignalR

- Stayed within our comfort zone

What's next?

- Show you the big bang idea

- First a distraction..."
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
        Styling.RenderTitle("Stage complete", canvas, dimensions);

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

        if (currentTransition >= 4)
        {
            var imageHeight = image.Height * 0.3f;
            canvas.DrawImage(
                image,
                dimensions.Center.X,
                dimensions.Center.Y - imageHeight / 2,
                image.Width * 0.3f,
                imageHeight);
        }

        base.Render(canvas, dimensions);
    }
}
