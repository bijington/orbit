using BuildingGames.GameObjects;

namespace BuildingGames.Slides.Gaming;

public class GamingTutorial6Scene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;

	public GamingTutorial6Scene(Pointer pointer) : base(pointer)
    {
        image = LoadImage("gaming_tutorial_6.png");
	}

    public override string Notes => 
        @"";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Client - Add Nuget Package", canvas, dimensions);

        var imageWidth = image.Width;
        var imageHeight = image.Height;

        canvas.DrawImage(image, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);

        var a = """
// dotnet CLI
dotnet add package Microsoft.AspNetCore.SignalR.Client --version 8.0.1

// PackageReference
<PackageReference Include="Microsoft.AspNetCore.SignalR.Client" Version="8.0.1" />
""";
    }
}
