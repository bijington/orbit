using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class PrologueScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 4;

    private readonly Microsoft.Maui.Graphics.IImage bookCover;
    private readonly Microsoft.Maui.Graphics.IImage nes;
    private readonly IList<string> textTransitions;

    public PrologueScene(Pointer pointer) : base(pointer)
    {
        bookCover = LoadImage("book_cover.jpg");
        nes = LoadImage("nes.jpg");

        textTransitions = new List<string>
        {
            @"- Childhood love of gaming and reading",

            @"- Childhood love of gaming and reading

- Content is built into an application/game",

            @"- Childhood love of gaming and reading

- Content is built into an application/game

- Carried away with features",

            @"- Childhood love of gaming and reading

- Content is built into an application/game

- Carried away with features

- You decide the content",

            @"- Childhood love of gaming and reading

- Content is built into an application/game

- Carried away with features

- You decide the content

- Win a prize"
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
        Styling.RenderTitle("Prologue", canvas, dimensions);

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

        if (currentTransition == 0)
        {
            canvas.DrawImage(nes, dimensions.Width * 0.65f, dimensions.Height * 0.18f, nes.Width * 0.3f, nes.Height * 0.3f);
        }

        if (currentTransition == 4)
        {
            //827 × 1254
            var bookWidth = bookCover.Width * 0.3f;
            canvas.DrawImage(bookCover, dimensions.Center.X - bookWidth / 2, dimensions.Height * 0.5f, bookWidth, bookCover.Height * 0.3f);
        }

        base.Render(canvas, dimensions);
    }
}
