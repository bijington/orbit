using BuildingGames.GameObjects;
using ChooseYourOwnAdventure.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorialWrapUp2Scene : SlideSceneBase
{
	public GamingTutorialWrapUp2Scene(Pointer pointer) : base(pointer)
    {
        Character.Position = Character.Positions.Decision2;
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Considerations - recommendations", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- Background jobs
https://learn.microsoft.com/azure/well-architected/reliability/background-jobs

- Background job queueing
https://learn.microsoft.com/aspnet/core/fundamentals/host/hosted-services

- PlayFab
https://playfab.com/multiplayer/#servers
""",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }
}
