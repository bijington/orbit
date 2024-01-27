using BuildingGames.GameObjects;
using Orbit.Engine;

namespace BuildingGames.Slides;

public abstract class SlideSceneBase : GameScene
{
    public SlideSceneBase(Pointer pointer)
    {
        Add(pointer);

        SlideDeck.Notes = this.Notes;
    }

    public virtual string BackgroundMusic { get; }

    public virtual string Notes { get; }

    public virtual bool CanGoBack { get; } = true;

    public virtual bool CanProgress { get; } = true;

    public virtual void GoBack()
    {
        this.Back?.Invoke(this);
    }

    public virtual void Progress()
    {
        this.Next?.Invoke(this);
    }

    public event Action<SlideSceneBase> Back;

    public event Action<SlideSceneBase> Next;

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        var font = Styling.Font;

        canvas.Font = font;
        canvas.FontSize = Styling.FooterSize;
        canvas.FontColor = Styling.FooterColor;

        canvas.DrawString(
            dimensions,
            "Shaun Lawrence",
            Styling.TitleColor,
            Colors.Transparent,
            0.5f,
            font,
            Styling.FooterSize,
            new PointF(10, dimensions.Bottom - 60),
            HorizontalAlignment.Left,
            VerticalAlignment.Top);

        canvas.DrawString(
            dimensions,
            "@Bijington",
            Styling.TitleColor,
            Colors.Transparent,
            0.5f,
            font,
            Styling.FooterSize,
            new PointF(-10, dimensions.Bottom - 60),
            HorizontalAlignment.Right,
            VerticalAlignment.Top);

        canvas.Alpha = 1f;
    }
}
