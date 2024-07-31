using Microsoft.Maui.Graphics.Skia;

using SkiaSharp;

namespace Orbit.Studio.Sprites;

public partial class SpriteEditorPage : ContentPage, IDrawable
{
    public SpriteEditorPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private int width = 16;
    private int height = 16;
    private IList<Pixel> pixels = [];
    private float mouseX;
    private float mouseY;

    private void GraphicsView_OnEndInteraction(object? sender, TouchEventArgs e)
    {
        pixels.Add(new Pixel { Color = ColorPicker.SelectedColor, Location = new PointF(mouseX, mouseY) });
    }
        
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Render(canvas, dirtyRect, (float)Zoom.Value, ShowGridLines.IsChecked, ShowChessboard.IsChecked);
    }

    private void Render(ICanvas canvas, RectF bounds, float zoomFactor, bool showGridLines, bool showChessboard)
    {
        var renderedWidth = width * zoomFactor;
        var renderedHeight = height * zoomFactor;

        if (showChessboard)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var number = y % 2 == 0 ? 1 : 0;
                    canvas.FillColor = x % 2 == number ? Colors.White : Color.FromRgb(192 / 255d, 192 / 255d, 192 / 255d);
                    canvas.FillRectangle(x * zoomFactor, y * zoomFactor, 1 * zoomFactor, 1 * zoomFactor);
                }
            }
        }

        if (showGridLines)
        {
            var lineColor = Color.FromRgb(192 / 255d, 192 / 255d, 192 / 255d);
            
            for (int x = 1; x < width; x++)
            {
                canvas.StrokeColor = lineColor;
                canvas.DrawLine(x * zoomFactor, 0, x * zoomFactor, height * zoomFactor);
            }
            
            for (int y = 1; y < height; y++)
            {
                canvas.StrokeColor = lineColor;
                canvas.DrawLine(0, y * zoomFactor, width * zoomFactor, y * zoomFactor);
            }
        }

        foreach (var tile in pixels)
        {
            canvas.FillColor = tile.Color;
            canvas.FillRectangle(tile.Location.X * zoomFactor, tile.Location.Y * zoomFactor, zoomFactor, zoomFactor);    
        }
        
        canvas.FillColor = ColorPicker.SelectedColor;
        canvas.FillRectangle(mouseX * zoomFactor, mouseY * zoomFactor, zoomFactor, zoomFactor);
    }

    private void Zoom_OnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        Canvas.Invalidate();
    }

    private void Canvas_OnMoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        var touch = e.Touches.First();

        var zoomFactor = (float)Zoom.Value;
        
        mouseX = MathF.Floor(touch.X / zoomFactor);
        mouseY = MathF.Floor(touch.Y / zoomFactor);
        
        Canvas.Invalidate();
    }
    
    private void Export()
    {
        using var canvas = new SkiaCanvas();
        using var bitmap = new SKBitmap(new SKImageInfo(width, height));
        canvas.Canvas = new SKCanvas(bitmap);
        
        Render(canvas, new RectF(0, 0, width, height), 1f, false, false);

        var path = Path.Combine(FileSystem.AppDataDirectory, @"sprite.png");
        using var stream = File.Create(path);
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        Export();
    }

    private void ShowGridLines_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        Canvas.Invalidate();
    }

    private void ShowChessboard_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        Canvas.Invalidate();
    }

    private void OnUndoClicked(object? sender, EventArgs e)
    {
        pixels.RemoveAt(pixels.Count - 1);
        Canvas.Invalidate();
    }

    private void WidthEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        int.TryParse(e.NewTextValue, out width);
        Canvas.Invalidate();
    }
    
    private void HeightEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        int.TryParse(e.NewTextValue, out height);
        Canvas.Invalidate();
    }
}

public class Pixel
{
    public Color Color { get; init; }
        
    public PointF Location { get; init; }
}