using BuildingGames.GameObjects;
using ChooseYourOwnAdventure;
using ChooseYourOwnAdventure.GameObjects;
using Orbit.Engine;

namespace BuildingGames.Slides;

public class HowToUsePartFive : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly float aspectRatio;
    private readonly Bat bat;

    public HowToUsePartFive(Pointer pointer, Bat bat) : base(pointer)
    {
        image = LoadImage("how_to_part_five.png");
        aspectRatio = image.Width / image.Height;

        Character.Position = Character.Positions.Decision1;
        this.bat = bat;

        Add(bat);
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("How to use - GameObject", canvas, dimensions);

        var imageHeight = dimensions.Height * 0.7f;
        var imageWidth = imageHeight * aspectRatio;

        canvas.DrawImage(
            image,
            100,
            dimensions.Height - imageHeight * 1.2f,
            imageWidth,
            imageHeight);

        var batX = imageWidth + 200;
        var batSize = 300;

        bat.Bounds = new RectF(batX, dimensions.Center.Y - batSize / 2, batSize, batSize);

        base.Render(canvas, dimensions);
    }
}
