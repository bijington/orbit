using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Tutorials;

public class TutorialPartTwoScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

    public override string Notes => 
        @"The second key component is SignalR.

- Open-source

- ASP.NET Core

- Real-time Bi-directional communication

- Automatic connection management

- Scalable

- Manages complexity for us
    - WebSockets
    - Server-Sent events
    - Long Polling

- Supports many platforms - JS, C#, F#, VB, Java";

    public TutorialPartTwoScene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("signalr.png");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Tutorial - What is SignalR?", canvas, dimensions);
        // Who here has a creative itch?
//Growing up I used to love diving into a ‘choose your own adventure’ style book and then later on in life, the same concept in video game form.I would like to apply this concept in today’s talk… 

//Join me in learning about how we can build video games with.NET MAUI in the form of a ‘choose your own adventure’ style game. You as the collective audience will be able to choose the paths that we go down and influence the content that gets presented.

//Learn through our own voting system how we can combine technology such as SignalR to provide real time multi - player support into our.NET MAUI based games as well as many other cool techniques to really make our games or applications feel alive.

        canvas.DrawString(
            dimensions,
            @"- Open-source

- ASP.NET Core

- Real-time communication

- Bi-directional connection

- Scalable

- Manages complexity for us

- Supports many platforms - JS, C#, F#, VB, Java"
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
            dimensions.Width * 0.4f,
            dimensions.Height / 4,
            image.Width * 2.5f,
            image.Height * 2.5f);

        base.Render(canvas, dimensions);
    }
}
