using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class PrologueScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 4;

    private readonly Microsoft.Maui.Graphics.IImage bookCover;
    private readonly Microsoft.Maui.Graphics.IImage nes;
    private readonly IList<string> textTransitions;

    public override string Notes => 
"""
I have been very passionate about gaming ever since saving up to buy my first ever console back in 1992 - the NES. That in combination of a plentiful supply of second hand choose your own adventure books from my nan has helped get me here today.
I love how all aspects of video games have the potential to create worlds that we can get lost in, or even inspire us to be creative.
On the subject of creativity, I remember reading a book very early on in my career that really shaped my thinking. It was called ‘Pragmatic Thinking and Learning' and it was written by Andrew Hunt. In the book he talks about how the brain is split into two parts, the linear and the rich. The linear side is the logical side and the rich side is the creative side. He goes on to discuss how deliberately switching between the two sides can help us to solve problems.
The process involves effectively taking a break from solving a logical problem and expose yourself to something creative. And it is this process that I have used to help me solve many problems over the years.

There are some things that I would like to introduce you all to before we get going on the content.

The first is that all of the content you see today is either baked into a .NET MAUI based game or code within Visual Studio.

Heading down this route, I really struggled to prevent myself from getting carried away with functionality in the hope that the system presenting the content also makes up part of the content itself.

Another point, and while this have been a nice selling point on my talk submission is has proven to be somewhat of a challenge... you will be influencing the content that gets shown today. Through the magic of SignalR you will be able to navigate to a blazor based web page and when each decision point is reached, cast your vote. I should add that I owe a big thanks to a good friend Gerald for having already built some of the SignalR voting system. Thanks to the wonders of open-source I was able to fork it and bend it to suit my needs. One decision point will actually allow you to choose to understand the voting system itself.

And finally, I mentioned before that I would be giving a copy of my book away today. After voting on the first decision point one lucky voter will be shown a congratulations message. The message does say to come down and collect your prize, it might be best to take a screenshot and then come down
at the end of the talk.
""";

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

        //if (currentTransition == 0)
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
