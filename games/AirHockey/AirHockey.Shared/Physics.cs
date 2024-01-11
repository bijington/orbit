namespace AirHockey.Shared;

public static class Physics
{
    public static bool DoCirclesIntersect(double x1, double y1, double r1, double x2, double y2, double r2)
    {
        // double x1 = one.Center.X;
        // double x2 = two.Center.X;
        // double y1 = one.Center.Y;
        // double y2 = two.Center.Y;

        // double r1 = one.Width / 2;
        // double r2 = two.Width / 2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        return d <= Math.Abs(r1 - r2) || d <= r1 + r2;
    }
}
