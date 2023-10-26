namespace BuildingGames;

public class ContrastRatioComparer
{
    public static double GetContrastRatio(Color lighterColor, Color darkerColor)
    {
        var l1 = GetRelativeLuminance(lighterColor);
        var l2 = GetRelativeLuminance(darkerColor);


        return (l1 + 0.05) / (l2 + 0.05);
    }


    private static double GetRelativeLuminance(Color color)
    {
        var r = GetRelativeComponent(color.Red);
        var g = GetRelativeComponent(color.Green);
        var b = GetRelativeComponent(color.Blue);


        return
            0.2126 * r +
            0.7152 * g +
            0.0722 * b;
    }


    private static double GetRelativeComponent(float component)
    {
        if (component <= 0.03928)
        {
            return component / 12.92;
        }


        return Math.Pow(((component + 0.055) / 1.055), 2.4);
    }
}
