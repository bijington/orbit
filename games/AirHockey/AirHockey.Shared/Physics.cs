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

    // apply force and angle to an object after colliding with another object
    public static void ApplyForceAfterCollision(PlayerState one, PuckState two, double angle)
    {
        var r1 = one.Size / 2;
        var r2 = two.Size / 2;

        double x1 = one.X + r1;
        double x2 = two.X + r2;
        double y1 = one.Y + r1;
        double y2 = two.Y  + r2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        Console.WriteLine($"d = {d}");

        double nx = (x2 - x1) / d;
        double ny = (y2 - y1) / d;

        double p = 2 * ((2 * two.VelocityX) * nx + (2 * two.VelocityY) * ny - two.VelocityX * nx - two.VelocityY * ny) / (one.Mass + two.Mass);

        Console.WriteLine($"p = {p}");
        // one.Velocity.X = one.Velocity.X - p * one.Mass * nx;
        // one.Velocity.Y = one.Velocity.Y - p * one.Mass * ny;
        two.VelocityX = two.VelocityX + p * two.Mass * nx;
        two.VelocityY = two.VelocityY + p * two.Mass * ny;

        two.VelocityX = Math.Cos(angle * Math.PI / 180) * two.VelocityX - Math.Sin(angle * Math.PI / 180) * two.VelocityY;
        two.VelocityY = Math.Sin(angle * Math.PI / 180) * two.VelocityX + Math.Cos(angle * Math.PI / 180) * two.VelocityY;
    }

    // // apply force to an object after colliding with another object
    // public static void ApplyForceAfterCollision(GameObject one, GameObject two)
    // {
    //     double x1 = one.Center.X;
    //     double x2 = two.Center.X;
    //     double y1 = one.Center.Y;
    //     double y2 = two.Center.Y;

    //     double r1 = one.Width / 2;
    //     double r2 = two.Width / 2;

    //     double d = Math.Sqrt((x1 - x2) * (x1 - x2)
    //                         + (y1 - y2) * (y1 - y2));

    //     double nx = (x2 - x1) / d;
    //     double ny = (y2 - y1) / d;

    //     double p = 2 * (one.Velocity.X * nx + one.Velocity.Y * ny - two.Velocity.X * nx - two.Velocity.Y * ny) / (one.Mass + two.Mass);

    //     one.Velocity.X = one.Velocity.X - p * one.Mass * nx;
    //     one.Velocity.Y = one.Velocity.Y - p * one.Mass * ny;
    //     two.Velocity.X = two.Velocity.X + p * two.Mass * nx;
    //     two.Velocity.Y = two.Velocity.Y + p * two.Mass * ny;
    // }

    // calculate angle of movement after collision
    public static double CalculateAngleAfterCollision(PlayerState one, PuckState two)
    {
        var r1 = one.Size / 2;
        var r2 = two.Size / 2;

        double x1 = one.X + r1;
        double x2 = two.X + r2;
        double y1 = one.Y + r1;
        double y2 = two.Y  + r2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        double nx = (x2 - x1) / d;
        double ny = (y2 - y1) / d;

        double p = 2 * ((2 * two.VelocityX) * nx + (2 * two.VelocityY) * ny - two.VelocityX * nx - two.VelocityY * ny) / (one.Mass + two.Mass);

        double vx1 = 1 - p * one.Mass * nx;
        double vy1 = 1 - p * one.Mass * ny;
        double vx2 = two.VelocityX + p * two.Mass * nx;
        double vy2 = two.VelocityY + p * two.Mass * ny;

        double angle = Math.Atan2(vy1, vx1) * 180 / Math.PI;

        return angle;
    }
}
