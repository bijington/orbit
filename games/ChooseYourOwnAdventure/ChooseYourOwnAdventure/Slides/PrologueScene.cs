using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class PrologueScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage bookCover;

    public PrologueScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
        bookCover = LoadImage("book_cover.jpg");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Prologue", canvas, dimensions);
        // Who here has a creative itch?
//Growing up I used to love diving into a ‘choose your own adventure’ style book and then later on in life, the same concept in video game form.I would like to apply this concept in today’s talk… 

//Join me in learning about how we can build video games with.NET MAUI in the form of a ‘choose your own adventure’ style game. You as the collective audience will be able to choose the paths that we go down and influence the content that gets presented.

//Learn through our own voting system how we can combine technology such as SignalR to provide real time multi - player support into our.NET MAUI based games as well as many other cool techniques to really make our games or applications feel alive.

        canvas.DrawString(
            dimensions,
            @"- Childhood love of gaming and reading

- Content is built into an application/game

- Carried away with features

- You decide the content

- Win a prize"
            ,
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        //827 × 1254
        canvas.DrawImage(bookCover, dimensions.Width / 5, dimensions.Height * 0.65f, bookCover.Width * 0.3f, bookCover.Height * 0.3f);

        base.Render(canvas, dimensions);
    }
}
