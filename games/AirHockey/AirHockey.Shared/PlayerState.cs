namespace AirHockey.Shared;

public class PlayerState
{
    public PlayerState(Guid id)
    {
        Id = id;
        Size = 0.025;
    }

    public Guid Id { get; }

    public Guid GameId { get; internal set; }

    public double X { get; set; }

    public double Y { get; set; }

    public double Size { get; }

    public double Mass { get; } = 2;

    public bool IsBottom { get; set; }

    public static PlayerState Empty { get; } = new PlayerState(Guid.Empty);
}