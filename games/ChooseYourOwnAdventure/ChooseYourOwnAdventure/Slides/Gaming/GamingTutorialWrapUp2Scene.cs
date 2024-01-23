using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorialWrapUp2Scene : SlideSceneBase, IDestinationKnowingScene
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorialWrapUp2Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_wrapup_1.jpg");
	}

    public Type DestinationSceneType => typeof(Stage1SummaryScene);

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

- MessagePack Hub Protocol
https://learn.microsoft.com/aspnet/core/signalr/messagepackhubprotocol

- Use strongly typed hubs
https://learn.microsoft.com/aspnet/core/signalr/hubs#strongly-typed-hubs

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

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Right - imageWidth - 80, dimensions.Bottom - imageHeight - 80, imageWidth, imageHeight);

        base.Render(canvas, dimensions);
    }
}
