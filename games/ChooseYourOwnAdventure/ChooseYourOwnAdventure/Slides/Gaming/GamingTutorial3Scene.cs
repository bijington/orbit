using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial3Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial3Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_3.png");
	}

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Registering the hub", canvas, dimensions);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);

        var a = @"
var builder = WebApplication.CreateBuilder(args);

// Register SignalR dependencies.
builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

// Map our hub implementation to /Game from the main url.
app.MapHub<GameHub>(""Game"");

app.Run();";
    }
}