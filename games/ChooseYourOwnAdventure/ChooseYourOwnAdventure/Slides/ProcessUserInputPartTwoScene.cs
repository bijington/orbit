using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class ProcessUserInputPartTwoScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage controller;

    public ProcessUserInputPartTwoScene(Pointer pointer) : base(pointer)
    {
        controller = LoadImage("game_controller.png");
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Process user input", canvas, dimensions);

        var imageWidth = controller.Width * 0.6f;
        var imageHeight = controller.Height * 0.6f;

        canvas.DrawImage(controller, dimensions.Center.X - imageWidth / 2, dimensions.Center.Y - imageHeight / 2, imageWidth, imageHeight);

        base.Render(canvas, dimensions);
    }
}
