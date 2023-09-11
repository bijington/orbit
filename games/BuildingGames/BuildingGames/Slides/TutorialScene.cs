using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TutorialScene : SlideSceneBase
{
	private int currentTransition = 0;
	private const int transitions = 1;

	public TutorialScene(Pointer pointer, Achievement achievement) : base(pointer, achievement)
    {
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
        Styling.RenderTitle("Tutorial", canvas, dimensions);

        if (currentTransition > 0)
        {
            canvas.DrawString(
                dimensions,
                @"public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    builder
        .UseMauiApp<App>()
        .UseOrbitEngine();

    return builder.Build();
}",
                Styling.TitleColor,
                Colors.Transparent,
                1,
                Styling.CodeFont,
                25,
                new PointF(0, dimensions.Height * 0.75f),
                HorizontalAlignment.Left,
                VerticalAlignment.Top);
        }

        base.Render(canvas, dimensions);
    }
}
