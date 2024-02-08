using BuildingGames.GameObjects;

namespace BuildingGames.Slides;

public class TitleScene : SlideSceneBase
{
    private readonly AchievementManager achievementManager;
    private float startAlpha = 1f;
    private float increment = -0.05f;

    public TitleScene(Pointer pointer, AchievementManager achievementManager) : base(pointer)
    {
        this.achievementManager = achievementManager;
    }

    public override string Notes => 
"""
Welcome everyone, thank you all for joining me today. I hope you have all enjoyed the conference so far.
        
I am hoping to give some insight into a library that I have built, which aims to make it easier to build games in .NET MAUI. We will walk through how to use it from a conceptual view and then move onto a concrete example of your choice.
""";

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        canvas.DrawString(
            new RectF(40, 80, dimensions.Width - 80, dimensions.Height / 2),
            "Choose your own adventure",
            Styling.Secondary,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.17),
            new PointF(40, dimensions.Height * 0.2f),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        canvas.DrawString(
            new RectF(40, 80, dimensions.Width - 80, dimensions.Height / 2),
            "Building games in .NET MAUI",
            Styling.Tertiary,
            Colors.Transparent,
            1,
            Styling.Font,
            (float)Styling.ScaledFontSize(0.1),
            new PointF(40, dimensions.Height * 0.5f),
            HorizontalAlignment.Center,
            VerticalAlignment.Center);

        canvas.DrawString(
            dimensions,
            "PRESS START",
            Colors.Yellow,
            Colors.Transparent,
            startAlpha,
            Styling.Font,
            (float)Styling.CodeSize,
            new PointF(0, dimensions.Height * 0.75f),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        // Undo our alpha change
        canvas.Alpha = 1.0f;

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        startAlpha = Math.Clamp(startAlpha + increment, 0f, 1f);

        if (startAlpha == 0f)
        {
            increment = 0.05f;
        }
        else if (startAlpha == 1f)
        {
            increment = -0.05f;
        }

        achievementManager.UpdateProgress(AchievementNames.FirstDecision, 100);
    }
}
