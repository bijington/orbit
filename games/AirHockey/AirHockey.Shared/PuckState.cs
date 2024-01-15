namespace AirHockey.Shared;

public class PuckState
{
    public PuckState()
    {
        Size = 0.02;
    }

    public double X { get; set; }

    public double Y { get; set; }

    public double Size { get; }

    public double VelocityX { get; set; }

    public double VelocityY { get; set; }

    public double Mass { get; } = 0.001;
}