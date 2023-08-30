using System.Diagnostics;
using BuildingGames.GameObjects;
using Orbit.Engine;

namespace BuildingGames.Scenes;

public class FreeMainScene : SlideSceneBase
{
    private readonly FreeBoarder player;
    private readonly StateObject stateObject;
    private readonly Microsoft.Maui.Graphics.IImage river;
    private float riverX;
    private float river2X;

    public override bool CanProgress => true;

    public FreeMainScene(
        FreeBoarder player,
        StateObject stateObject,
        DistanceCounter distanceCounter,
        Log log)
    {
        Add(player);
        Add(stateObject);
        Add(distanceCounter);
        Add(log);

        this.player = player;
        this.stateObject = stateObject;
        river = LoadImage("river.png");

        riverX = 0;
        river2X = river.Width;

        player.RiverHeight = river.Height;
    }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        // Draw the land
        canvas.FillColor = Color.FromArgb("#3ABE41");
        canvas.FillRectangle(dimensions);

        // And then the flowing river
        var y = (dimensions.Height - river.Height) / 2;

        // Only draw if the image is on screen.
        if (dimensions.IntersectsWith(new RectF(riverX, y, river.Width, river.Height)))
        {
            canvas.DrawImage(
                river,
                riverX,
                y,
                river.Width,
                river.Height);
        }
        if (dimensions.IntersectsWith(new RectF(river2X, y, river.Width, river.Height)))
        {
            canvas.DrawImage(
            river,
            river2X,
            y,
            river.Width,
            river.Height);
        }

        var font = Styling.Font;

        canvas.Font = font;
        canvas.FontSize = 80;
        canvas.FontColor = Colors.White;
        canvas.SetShadow(new SizeF(5, 5), 5, Colors.Black);

        canvas.DrawString(
            new RectF(40, dimensions.Height * 0.08f, dimensions.Width - 80, dimensions.Height),
            "Apply weight",
            font,
            40,
            new PointF(40, dimensions.Height * 0.08f),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);

        base.Render(canvas, dimensions);
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        var change = stateObject.CurrentFlowRate / (float)millisecondsSinceLastUpdate;
        Debug.WriteLine($"FreeMainScene - {change}");
        riverX -= change;
        river2X -= change;

        if (riverX + river.Width < 0)
        {
            riverX = river2X + river.Width;
        }

        if (river2X + river.Width < 0)
        {
            river2X = riverX + river.Width;
        }
    }
}
