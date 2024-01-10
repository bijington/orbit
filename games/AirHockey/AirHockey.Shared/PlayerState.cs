namespace AirHockey.Shared;

public class PlayerState
{
    public PlayerState(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public double X { get; set; }

    public double Y { get; set; }

    public double Size { get; set; }

    public static PlayerState Empty { get; } = new PlayerState(Guid.Empty);
}