using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class HowToUsePartFour : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage image;
    private readonly float aspectRatio;

    public HowToUsePartFour(Pointer pointer, AchievementBanner achievement) : base(pointer, achievement)
    {
        image = LoadImage("how_to_part_four.png");
        aspectRatio = image.Width / image.Height;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("How to use - GameSceneManager", canvas, dimensions);

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
