using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class ProcessUserInputScene : SlideSceneBase
{
    private readonly Microsoft.Maui.Graphics.IImage controller;
    private readonly Microsoft.Maui.Graphics.IImage touch;
    private readonly Microsoft.Maui.Graphics.IImage keyboard;
    private readonly Microsoft.Maui.Graphics.IImage gyroscope;

    public ProcessUserInputScene(Pointer pointer) : base(pointer)
    {
        controller = LoadImage("controller.png");
        touch = LoadImage("touch.png");
        keyboard = LoadImage("keyboard.png");
        gyroscope = LoadImage("gyroscope.png");
    }

    public override string Notes => 
"""
The processing of user input can come in many different forms. Thanks to the cross platform nature of .NET MAUI we can support many different input types.

- Game controller support

  - Android - Paddleboat
    
  - iOS/MacCatalyst - GCController
    
  - Windows

Touch/Mouse

Keyboard

Accelerometer / gyroscope
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        Styling.RenderTitle("Process user input", canvas, dimensions);


        canvas.DrawString(
            dimensions,
            @"- Game controller support

  - Android - Paddleboat
    
  - iOS/MacCatalyst - GCController
    
  - Windows

- Touch

- Keyboard

- Accelerometer / gyroscope",
            Styling.TitleColor,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.048),
            new PointF(40, dimensions.Height * 0.18f),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        var y = dimensions.Height * 0.18f;
        var availableVerticalSpace = dimensions.Height - 2 * y;
        var padding = (availableVerticalSpace - (controller.Height * 0.8f * 4)) / 3;
        canvas.DrawImage(controller, dimensions.Width * 0.75f, y, controller.Width * 0.8f, controller.Height * 0.8f);

        y += controller.Height * 0.8f + padding;

        canvas.DrawImage(touch, dimensions.Width * 0.75f, y, touch.Width * 0.8f, touch.Height * 0.8f);

        y += controller.Height * 0.8f + padding;

        canvas.DrawImage(keyboard, dimensions.Width * 0.75f, y, keyboard.Width * 0.8f, keyboard.Height * 0.8f);

        y += controller.Height * 0.8f + padding;

        canvas.DrawImage(gyroscope, dimensions.Width * 0.75f, y, gyroscope.Width * 0.8f, gyroscope.Height * 0.8f);

        base.Render(canvas, dimensions);
    }
}
