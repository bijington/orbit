using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TutorialScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

    public TutorialScene(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
        image = LoadImage("dotnet_bot_iot.png");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Tutorial - What is .NET MAUI?", canvas, dimensions);
        // Who here has a creative itch?
//Growing up I used to love diving into a ‘choose your own adventure’ style book and then later on in life, the same concept in video game form.I would like to apply this concept in today’s talk… 

//Join me in learning about how we can build video games with.NET MAUI in the form of a ‘choose your own adventure’ style game. You as the collective audience will be able to choose the paths that we go down and influence the content that gets presented.

//Learn through our own voting system how we can combine technology such as SignalR to provide real time multi - player support into our.NET MAUI based games as well as many other cool techniques to really make our games or applications feel alive.

        canvas.DrawString(
            dimensions,
            @"- Multi-platform App UI

- Cross-platform framework

  - Mobile - Android and iOS

  - Desktop - macOS and Windows

  - Smart Samsung things - Tizen

- Evolution of Xamarin.Forms

  - First class features such as AppBuilder, etc.

- Build with the platform"
            ,
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        canvas.DrawImage(
            image,
            dimensions.Width * 0.65f,
            dimensions.Height / 4,
            image.Width * 2.5f,
            image.Height * 2.5f);

        base.Render(canvas, dimensions);
    }
}
