namespace Orbit;

public class Movement
{
    private readonly PointF origin;
    private readonly PointF destination;
    private readonly PointF speed;

    public float DestinationX => destination.X;

    public float DestinationY => destination.Y;

    public float OriginX => origin.X;

    public float OriginY => origin.Y;

    public float SpeedX => speed.X;

    public float SpeedY => speed.Y;

    public Movement(
		PointF origin,
		PointF destination,
		PointF speed)
	{
        this.origin = origin;
        this.destination = destination;
        this.speed = speed;
    }

    // TODO: Should this handle the actual change in bounds for the GameObject also?
}
