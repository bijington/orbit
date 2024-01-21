using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial3Scene : SlideSceneBase
{
	public GamingTutorial3Scene(Pointer pointer) : base(pointer)
    {
	}

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Registering the hub", canvas, dimensions);

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
