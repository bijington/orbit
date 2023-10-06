using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class VotingSignalRScene : SlideSceneBase
{
    private int currentTransition = 0;
    private const int transitions = 8;

    public VotingSignalRScene(Pointer pointer) : base(pointer)
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
        Styling.RenderTitle("Controlling the votes", canvas, dimensions);

        // Open connection
        // Subscribe to UpdateVotes
        // Send OpenVoting
        // Send CloseVoting

        // Blazor clients connect
        // Ask for status

        var height = dimensions.Height / 6;
        var width = dimensions.Width / 6;
        var signalRBounds = new RectF(dimensions.Center.X - width / 2, dimensions.Height * 0.25f, width, height);

        var hubHeight = dimensions.Height / 8;
        var hubWidth = dimensions.Width / 8;
        var hubBounds = new RectF(dimensions.Center.X - hubWidth / 2, dimensions.Height * 0.25f + (height * 0.75f), hubWidth, hubHeight);

        var device1Height = dimensions.Height / 4;
        var device1Width = dimensions.Width / 14;
        var device1Bounds = new RectF(dimensions.Width / 6, dimensions.Center.Y - device1Height / 2, device1Width, device1Height);

        var device2Bounds = new RectF(dimensions.Width * (5f / 6f) - device1Width, dimensions.Center.Y - device1Height / 2, device1Width, device1Height);

        var device3Bounds = new RectF(dimensions.Center.X - device1Width / 2, (dimensions.Height * 0.75f) - device1Height / 2, device1Width, device1Height);

        // SignalR Server
        canvas.FillColor = Styling.Secondary;
        canvas.FillEllipse(signalRBounds);

        canvas.FontSize = (float)Styling.ScaledFontSize(0.05f);
        canvas.FontColor = Styling.Primary;
        canvas.DrawString(
            "SignalR Server",
            signalRBounds,
            HorizontalAlignment.Center,
            VerticalAlignment.Center,
            TextFlow.OverflowBounds);

        if (currentTransition > 1)
        {
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 40;
            canvas.DrawLine(hubBounds.Center, device1Bounds.Center);
        }

        if (currentTransition > 6)
        {
            // Device to hub connection
            canvas.StrokeColor = Colors.Yellow;
            canvas.StrokeSize = 6;
            canvas.StrokeDashPattern = new[] { 10f, 5f, 2f, 2f };
            canvas.DrawLine(hubBounds.Center, device1Bounds.Center);
        }

        if (currentTransition > 5)
        {
            canvas.StrokeDashPattern = null;

            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 40;
            canvas.DrawLine(device2Bounds.Center, hubBounds.Center);

            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 40;
            canvas.DrawLine(device3Bounds.Center, hubBounds.Center);
        }

        if (currentTransition > 7)
        {
            // Device to hub connection
            canvas.StrokeColor = Colors.Yellow;
            canvas.StrokeSize = 6;
            canvas.StrokeDashPattern = new[] { 10f, 5f, 2f, 2f };
            canvas.DrawLine(device2Bounds.Center, hubBounds.Center);

            canvas.StrokeColor = Colors.Yellow;
            canvas.StrokeSize = 6;
            canvas.StrokeDashPattern = new[] { 10f, 5f, 2f, 2f };
            canvas.DrawLine(device3Bounds.Center, hubBounds.Center);
        }

        // Hub
        canvas.FillColor = Styling.Tertiary;
        canvas.FillRoundedRectangle(hubBounds, 30);

        canvas.FontSize = (float)Styling.ScaledFontSize(0.05f);
        canvas.FontColor = Styling.Primary;
        canvas.DrawString(
            "Hub",
            hubBounds,
            HorizontalAlignment.Center,
            VerticalAlignment.Center,
            TextFlow.OverflowBounds);

        if (currentTransition > 0)
        {
            // Device 1
            canvas.FillColor = Styling.Secondary;
            canvas.FillRoundedRectangle(device1Bounds, 20);

            canvas.FillColor = Styling.Primary;
            canvas.FillRectangle(device1Bounds.X + 10, device1Bounds.Y + 30, device1Bounds.Width - 20, device1Bounds.Height - 60);
        }

        if (currentTransition > 4)
        {
            // Device 2
            canvas.FillColor = Styling.Secondary;
            canvas.FillRoundedRectangle(device2Bounds, 20);

            canvas.FillColor = Styling.Primary;
            canvas.FillRectangle(device2Bounds.X + 10, device2Bounds.Y + 30, device2Bounds.Width - 20, device2Bounds.Height - 60);

            // Device 3
            canvas.FillColor = Styling.Secondary;
            canvas.FillRoundedRectangle(device3Bounds, 20);

            canvas.FillColor = Styling.Primary;
            canvas.FillRectangle(device3Bounds.X + 10, device3Bounds.Y + 30, device3Bounds.Width - 20, device3Bounds.Height - 60);
        }

        base.Render(canvas, dimensions);
    }
}
