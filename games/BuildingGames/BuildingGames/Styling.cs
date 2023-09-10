namespace BuildingGames;

public class Styling
{
	public static string FontName => "Josefin Sans Bold";

    public static Microsoft.Maui.Graphics.Font Font => new(FontName);

    public static Color FooterColor { get; } = Color.FromArgb("#A9F4D6");

    public static int FooterSize { get; } = 50;

    public static Color TitleColor { get; } = Color.FromArgb("#A9F4D6");

    public static double TitleSize { get; } = 150;

    public static void RenderTitle(string title, ICanvas canvas, RectF dimensions)
    {
        canvas.DrawString(
            new RectF(40, 0, dimensions.Width - 80, dimensions.Height / 2),
            title,
            TitleColor,
            Colors.Black,
            1,
            Font,
            (float)TitleSize,
            new PointF(40, 40),
            HorizontalAlignment.Center,
            VerticalAlignment.Top);
    }

    public static Color CodeColor { get; } = Color.FromArgb("#A9F4D6");

    public static double CodeSize { get; } = 43;//=> GetScreenDimensions().Height * 0.04;

    public static string CodeFontName => "Courier Prime";

    public static Microsoft.Maui.Graphics.Font CodeFont => new(CodeFontName);

    public static Color Tertiary = Color.FromArgb("#325A96");

    private static Size GetScreenDimensions()
    {
        var currentPage = Application.Current?.MainPage;

        if (currentPage is not null)
        {
            return new(currentPage.Width, currentPage.Height);
        }

        // If we can't find the current page dimensions then simply resort to the screen dimensions.
        return new (DeviceDisplay.MainDisplayInfo.Height, DeviceDisplay.MainDisplayInfo.Height);
    }
}
