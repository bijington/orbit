namespace AirHockey.Shared;

public class Game
{
    public Game(Guid id, PlayerState playerOne)
    {
        Id = id;
        PlayerOne = playerOne;
        PlayerTwo = PlayerState.Empty;
        PuckState = new();
        PuckState.X = 0.5;
        PuckState.Y = 0.5;
        PuckState.VelocityX = 0.01;
        PuckState.VelocityY = 0.001;
        ScoreState = new();
    }

    public Guid Id { get; }

    public PlayerState PlayerOne { get; set; }

    public PlayerState PlayerTwo { get; set; }

    public PuckState PuckState { get; }

    public ScoreState ScoreState { get; }
}