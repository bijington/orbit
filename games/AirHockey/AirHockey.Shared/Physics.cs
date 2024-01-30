namespace AirHockey.Shared;

public static class Physics
{
    public static bool DoCirclesIntersect(double x1, double y1, double r1, double x2, double y2, double r2)
    {
        // double x1 = playerState.Center.X;
        // double x2 = puckState.Center.X;
        // double y1 = playerState.Center.Y;
        // double y2 = puckState.Center.Y;

        // double r1 = playerState.Width / 2;
        // double r2 = puckState.Width / 2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        return d <= Math.Abs(r1 - r2) || d <= r1 + r2;
    }

    // apply force and angle to an object after colliding with another object
    // public static void ApplyForceAfterCollision(PlayerState playerState, PuckState puckState, double angle)
    // {
    //     var r1 = playerState.Size / 2;
    //     var r2 = puckState.Size / 2;

    //     double x1 = playerState.X;
    //     double x2 = puckState.X;
    //     double y1 = playerState.Y;
    //     double y2 = puckState.Y;

    //     double d = Math.Sqrt((x1 - x2) * (x1 - x2)
    //                         + (y1 - y2) * (y1 - y2));

    //     Console.WriteLine($"d = {d}");

    //     double nx = (x2 - x1) / d;
    //     double ny = (y2 - y1) / d;

    //     double p = 2 * ((2 * puckState.VelocityX) * nx + (2 * puckState.VelocityY) * ny - puckState.VelocityX * nx - puckState.VelocityY * ny) / (playerState.Mass + puckState.Mass);

    //     Console.WriteLine($"p = {p}");
    //     // playerState.Velocity.X = playerState.Velocity.X - p * playerState.Mass * nx;
    //     // playerState.Velocity.Y = playerState.Velocity.Y - p * playerState.Mass * ny;
    //     puckState.VelocityX = puckState.VelocityX + p * puckState.Mass * nx;
    //     puckState.VelocityY = puckState.VelocityY + p * puckState.Mass * ny;

    //     puckState.VelocityX = Math.Cos(angle * Math.PI / 180) * puckState.VelocityX - Math.Sin(angle * Math.PI / 180) * puckState.VelocityY;
    //     puckState.VelocityY = Math.Sin(angle * Math.PI / 180) * puckState.VelocityX + Math.Cos(angle * Math.PI / 180) * puckState.VelocityY;
    // }

    // // apply force to an object after colliding with another object
    public static void ApplyForceAfterCollision(PlayerState playerState, PuckState puckState)
    {
        double x1 = playerState.X;
        double x2 = puckState.X;
        double y1 = playerState.Y;
        double y2 = puckState.Y;

        double r1 = playerState.Size / 2;
        double r2 = puckState.Size / 2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        double nx = (x2 - x1) / d;
        double ny = (y2 - y1) / d;

        double p = 2 * (playerState.VelocityX * nx + playerState.VelocityY * ny - puckState.VelocityX * nx - puckState.VelocityY * ny) / (playerState.Mass + puckState.Mass);

        // playerState.VelocityX = playerState.VelocityX - p * playerState.Mass * nx;
        // playerState.VelocityY = playerState.VelocityY - p * playerState.Mass * ny;
        puckState.VelocityX = puckState.VelocityX + p * puckState.Mass * nx;
        puckState.VelocityY = puckState.VelocityY + p * puckState.Mass * ny;

        puckState.VelocityX = -puckState.VelocityX;
        puckState.VelocityY = -puckState.VelocityY;
    }

    // calculate angle of movement after collision
    public static double CalculateAngleAfterCollision(PlayerState playerState, PuckState puckState)
    {
        var r1 = playerState.Size / 2;
        var r2 = puckState.Size / 2;

        double x1 = playerState.X + r1;
        double x2 = puckState.X + r2;
        double y1 = playerState.Y + r1;
        double y2 = puckState.Y  + r2;

        double d = Math.Sqrt((x1 - x2) * (x1 - x2)
                            + (y1 - y2) * (y1 - y2));

        double nx = (x2 - x1) / d;
        double ny = (y2 - y1) / d;

        double p = 2 * ((2 * puckState.VelocityX) * nx + (2 * puckState.VelocityY) * ny - puckState.VelocityX * nx - puckState.VelocityY * ny) / (playerState.Mass + puckState.Mass);

        double vx1 = 1 - p * playerState.Mass * nx;
        double vy1 = 1 - p * playerState.Mass * ny;
        double vx2 = puckState.VelocityX + p * puckState.Mass * nx;
        double vy2 = puckState.VelocityY + p * puckState.Mass * ny;

        double angle = Math.Atan2(vy1, vx1) * 180 / Math.PI;

        return angle;
    }
}
