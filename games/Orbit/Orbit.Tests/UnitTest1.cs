using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics.Skia;
using NUnit.Framework;
using Orbit.Engine;
using Orbit.GameObjects;
using Orbit.Scenes;
using SkiaSharp;

namespace Orbit.Tests;

public class Tests
{
    [Test]
    public void SceneShouldRenderAsExpected()
    {
        VerifyScene(new Scenery(), 1000, 1000);
    }

    [Test]
    public void HomeSceneShouldRenderAsExpected()
    {
        var home = new HomeScene(new Ship(null, new Gun()), new Sun(), new Planet(null, new GameObjects.Shadow()));
        home.Update(0);
        VerifyScene(home, 1000, 1000);
    }

    // TODO: For some reason the asteroid is not rendering.
    [Test]
    public void AsteroidShouldRenderAsExpected()
    {
        var asteroid = new Asteroid(null);

        asteroid.SetMovement(new Movement(new PointF(0f, 0f), new PointF(0.5f, 0.5f), new PointF(0.01f, 0.01f)));

        for (int i = 0; i < 10; i++)
        {
            VerifyScene(asteroid, 1000, 1000, $"AsteroidShouldRenderAsExpected{i}");

            asteroid.Update(16);
        }
    }

    public static void VerifyScene(IRender render, int sceneWidth, int sceneHeight, [CallerMemberName] string caller = "")
    {
        using var canvas = new SkiaCanvas();
        using var bitmap = new SKBitmap(new SKImageInfo(sceneWidth, sceneHeight));
        canvas.Canvas = new SKCanvas(bitmap);

        render.Render(canvas, new Microsoft.Maui.Graphics.RectF(0, 0, sceneWidth, sceneHeight));

        // TODO: apply test name to the file.
        using var stream = File.Create($"{caller}.png");
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
