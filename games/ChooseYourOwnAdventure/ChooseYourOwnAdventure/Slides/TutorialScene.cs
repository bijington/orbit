using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TutorialScene : SlideSceneBase
{
    public TutorialScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
    {
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("- Prologue -", canvas, dimensions);
        // Who here has a creative itch?
//Growing up I used to love diving into a ‘choose your own adventure’ style book and then later on in life, the same concept in video game form.I would like to apply this concept in today’s talk… 

//Join me in learning about how we can build video games with.NET MAUI in the form of a ‘choose your own adventure’ style game. You as the collective audience will be able to choose the paths that we go down and influence the content that gets presented.

//Learn through our own voting system how we can combine technology such as SignalR to provide real time multi - player support into our.NET MAUI based games as well as many other cool techniques to really make our games or applications feel alive.

        canvas.DrawString(
            dimensions,
            @"- Love of gaming and reading

- Happy childhood memories

- Serial procrastination

- Scratch that creative itch"
            ,
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.CodeFont,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }
}
