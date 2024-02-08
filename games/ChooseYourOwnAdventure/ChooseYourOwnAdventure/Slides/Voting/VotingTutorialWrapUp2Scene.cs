using BuildingGames.GameObjects;
using ChooseYourOwnAdventure.GameObjects;

namespace BuildingGames.Slides.Voting;

public class VotingTutorialWrapUp2Scene : SlideSceneBase
{
	public VotingTutorialWrapUp2Scene(Pointer pointer) : base(pointer)
    {
        Character.Position = Character.Positions.Decision2;
	}

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Considerations - recommendations", canvas, dimensions);

        canvas.DrawString(
            dimensions,
"""
- MessagePack Hub Protocol
https://learn.microsoft.com/aspnet/core/signalr/messagepackhubprotocol

- Use strongly typed hubs
https://learn.microsoft.com/aspnet/core/signalr/hubs#strongly-typed-hubs
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
