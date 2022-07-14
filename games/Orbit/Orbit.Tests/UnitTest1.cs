using Microsoft.Maui.Graphics.Skia;
using NUnit.Framework;
using Orbit.Engine;
using Orbit.GameObjects;
using SkiaSharp;

namespace Orbit.Tests;

public class Tests
{
    [Test]
    public void SceneShouldRenderAsExpected()
    {
        VerifyScene(new Scenery(), 1000, 1000);
    }

    //[Test]
    //public void AsteroidShouldRenderAsExpected()
    //{
    //    VerifyScene(new Asteroid(null), 1000, 1000);
    //}

    public static void VerifyScene(IRender render, int sceneWidth, int sceneHeight)
    {
        using var canvas = new SkiaCanvas();
        using var bitmap = new SKBitmap(new SKImageInfo(sceneWidth, sceneHeight));
        canvas.Canvas = new SKCanvas(bitmap);

        render.Render(canvas, new Microsoft.Maui.Graphics.RectF(0, 0, sceneWidth, sceneHeight));

        using var stream = File.Create("snapshot.png");
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
    }

    public class Scenery : GameScene
    {
        public override void Render(ICanvas canvas, RectF dimensions)
        {
            base.Render(canvas, dimensions);

            canvas.FillColor = Colors.PaleGoldenrod;
            canvas.FillRectangle(new Rect(0, 0, 100, 100));
        }
    }
}
