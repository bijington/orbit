using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial7Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage[] images;
    private int currentTransition = 0;
    private const int transitions = 2;

	public GamingTutorial7Scene(Pointer pointer) : base(pointer)
    {
        images = [LoadImage("gaming_tutorial_7_0.png"), LoadImage("gaming_tutorial_7_1.png"), LoadImage("gaming_tutorial_7_2.png")];
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

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Client - Build a connection", canvas, dimensions);

        var image = images[currentTransition];

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);

        var a = """
// Build the connection.
var hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7226/Game")
    .Build();

// Setup callbacks from server.
hubConnection.On<PuckState>(EventNames.PuckStateUpdated, puckState =>
{
    PuckState = puckState;
});

// Start the connection.
await hubConnection.StartAsync();

// Send the initial state to the server.
await hubConnection.SendAsync(MethodNames.PlayGame, PlayerState.Id);
""";
    }
}
