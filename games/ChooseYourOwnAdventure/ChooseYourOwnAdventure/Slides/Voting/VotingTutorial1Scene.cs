using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Voting;

public class VotingTutorial1Scene : SlideSceneBase
{
    private int currentTransition = 0;
    private readonly int transitions = 8;

    private float[] communicationsStrokePattern = [10f, 5f, 2f, 2f];
    private const float communicationsStrokeSize = 6;
    private const float communicationsOffset = 20;
    private double communicationsElapsedMilliseconds = 0;

    public override string Notes => 
"""
So what are we building? 
""";

    public VotingTutorial1Scene(Pointer pointer, AchievementManager achievementManager) : base(pointer)
    {
        achievementManager.UpdateProgress(AchievementNames.KnowItAll, 100);

        transitions = (int)Enum.GetValues<Transition>().Max();
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

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        communicationsElapsedMilliseconds += millisecondsSinceLastUpdate;

        if (communicationsElapsedMilliseconds > 200)
        {
            var temp = communicationsStrokePattern[2];
            communicationsStrokePattern[2] = communicationsStrokePattern[0];
            communicationsStrokePattern[0] = temp;
            communicationsElapsedMilliseconds = 0;
        }
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("What are we building?", canvas, dimensions);

        var height = dimensions.Height / 6;
        var width = dimensions.Width / 6;
        var signalRBounds = new RectF(dimensions.Center.X - width / 2, dimensions.Height * 0.25f, width, height);

        var hubHeight = dimensions.Height / 8;
        var hubWidth = dimensions.Width / 8;
        var hubBounds = new RectF(dimensions.Center.X - hubWidth / 2, dimensions.Height * 0.25f + (height * 0.75f), hubWidth, hubHeight);

        var backgroundServiceHeight = dimensions.Height / 8;
        var backgroundServiceWidth = dimensions.Width / 8;
        var backgroundServiceBounds = new RectF(dimensions.Center.X - backgroundServiceWidth / 2, hubBounds.Bottom, backgroundServiceWidth, backgroundServiceHeight);

        var device1Height = dimensions.Height / 4;
        var device1Width = device1Height * 1.5f;
        var device1Bounds = new RectF(dimensions.Width / 6, dimensions.Center.Y - device1Height / 2, device1Width, device1Height);

        var device2Width = device1Height * 0.6f;
        var device2Bounds = new RectF(dimensions.Width * (5f / 6f) - device2Width, dimensions.Center.Y - device1Height / 2, device2Width, device1Height);

        var device1CommunicationsColor = Colors.Yellow;
        var device2CommunicationsColor = Colors.Red;

        if (currentTransition >= (int)Transition.SignalRServer)
        {
            // SignalR Server
            canvas.FillColor = Styling.Secondary;
            canvas.FillEllipse(signalRBounds);

            canvas.FontSize = (float)Styling.ScaledFontSize(0.05f);
            canvas.FontColor = Styling.Primary;
            canvas.DrawString(
                "Server",
                signalRBounds,
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.OverflowBounds);
        }

        if (currentTransition >= (int)Transition.DeviceToHubConnections)
        {
            canvas.StrokeDashPattern = null;
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 80;

            canvas.DrawLine(hubBounds.Center, device1Bounds.Center);
            canvas.DrawLine(device2Bounds.Center, hubBounds.Center);
        }

        if (currentTransition is (int)Transition.Device1ToDevice2Communications)
        {
            canvas.StrokeSize = communicationsStrokeSize;
            canvas.StrokeDashPattern = communicationsStrokePattern;
            
            canvas.StrokeColor = device1CommunicationsColor;
            canvas.DrawLine(hubBounds.Center.X - communicationsOffset, hubBounds.Center.Y - communicationsOffset, device1Bounds.Center.X - communicationsOffset, device1Bounds.Center.Y - communicationsOffset);
        }

        if (currentTransition is (int)Transition.Device1ToDevice2Communications)
        {
            DrawToDeviceCommunications(canvas, device2Bounds, hubBounds, device1CommunicationsColor);
        }

        if (currentTransition is (int)Transition.Device2ToDevice1Communications)
        {
            DrawFromDeviceCommunications(canvas, device2Bounds, hubBounds, device2CommunicationsColor);
            DrawToDeviceCommunications(canvas, device1Bounds, hubBounds, device2CommunicationsColor);
        }

        if (currentTransition >= (int)Transition.Hub)
        {
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
        }

        if (currentTransition >= (int)Transition.Devices)
        {
            // Device 1
            canvas.FillColor = Styling.Secondary;
            canvas.FillRoundedRectangle(device1Bounds, 20);

            canvas.FillColor = Styling.Primary;
            canvas.FillRectangle(device1Bounds.X + 10, device1Bounds.Y + 30, device1Bounds.Width - 20, device1Bounds.Height - 60);

            // Device 2
            canvas.FillColor = Styling.Secondary;
            canvas.FillRoundedRectangle(device2Bounds, 20);

            canvas.FillColor = Styling.Primary;
            canvas.FillRectangle(device2Bounds.X + 10, device2Bounds.Y + 30, device2Bounds.Width - 20, device2Bounds.Height - 60);

            canvas.FillColor = Styling.Secondary;
            canvas.FillRoundedRectangle(device2Bounds.X + 40, device2Bounds.Y + 40, device2Bounds.Width, device2Bounds.Height, 20);

            canvas.FillColor = Styling.Primary;
            canvas.FillRectangle(device2Bounds.X + 50, device2Bounds.Y + 80, device2Bounds.Width - 20, device2Bounds.Height - 60);
        }

        base.Render(canvas, dimensions);
    }

    private void DrawFromDeviceCommunications(ICanvas canvas, RectF deviceBounds, RectF hubBounds, Color color)
    {
        canvas.StrokeSize = communicationsStrokeSize;
        canvas.StrokeDashPattern = communicationsStrokePattern;

        canvas.StrokeColor = color;
        canvas.DrawLine(deviceBounds.Center.X - communicationsOffset, deviceBounds.Center.Y - communicationsOffset, hubBounds.Center.X - communicationsOffset, hubBounds.Center.Y - communicationsOffset);
    }

    private void DrawToDeviceCommunications(ICanvas canvas, RectF deviceBounds, RectF hubBounds, Color color)
    {
        canvas.StrokeSize = communicationsStrokeSize;
        canvas.StrokeDashPattern = communicationsStrokePattern;

        canvas.StrokeColor = color;
        canvas.DrawLine(deviceBounds.Center.X + communicationsOffset, deviceBounds.Center.Y + communicationsOffset, hubBounds.Center.X + communicationsOffset, hubBounds.Center.Y + communicationsOffset);
    }

    internal enum Transition
    {
        None,
        Devices,
        SignalRServer,
        Hub,
        DeviceToHubConnections,
        Device1ToDevice2Communications,
        Device2ToDevice1Communications,
    }
}
