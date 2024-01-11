namespace AirHockey.Shared;

public class GameState
{
    public GameState(Guid id, PlayerState playerOne, PlayerState playerTwo)
    {
        Id = id;
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
    }

    public Guid Id { get; }

    public PlayerState PlayerOne { get; set; }

    public PlayerState PlayerTwo { get; set; }
}