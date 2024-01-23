using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial5Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial5Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_5.png");
	}

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Registering the Background Service", canvas, dimensions);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);

        var a = @"
using AirHockey.Server;
using AirHockey.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Register the background service
builder.Services.AddHostedService<GameWorker>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();";
    }
}
