using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class HowToUsePartOne : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly float aspectRatio;

    public HowToUsePartOne(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
        image = LoadImage("how_to_part_one.png");
        aspectRatio = image.Width / image.Height;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("How to use - Initialisation", canvas, dimensions);

        var imageHeight = dimensions.Height * 0.7f;
        var imageWidth = imageHeight * aspectRatio;

        canvas.DrawImage(
            image,
            dimensions.Center.X - imageWidth / 2,
            dimensions.Height - imageHeight * 1.2f,
            imageWidth,
            imageHeight);

        base.Render(canvas, dimensions);
    }
}
