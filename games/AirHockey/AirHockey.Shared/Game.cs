namespace AirHockey.Shared;

public class Game
{
    private PlayerState playerOne;
    private PlayerState playerTwo;

    public Game(Guid id, PlayerState playerOne)
    {
        Id = id;
        PlayerOne = playerOne;
        PlayerTwo = PlayerState.Empty;
        PuckState = new();
        PuckState.X = 0.5;
        PuckState.Y = 0.5;
        PuckState.VelocityX = 0.000625;
        PuckState.VelocityY = 0.0000625;
        ScoreState = new();
    }

    public Guid Id { get; }

    public PlayerState PlayerOne
    {
        get => this.playerOne;
        set
        {
            this.playerOne = value;
            this.playerOne.GameId = Id;
        }
    }

    public PlayerState PlayerTwo
    {
        get => this.playerTwo;
        set
        {
            this.playerTwo = value;
            this.playerTwo.GameId = Id;
        }
    }

    public PuckState PuckState { get; }

    public ScoreState ScoreState { get; }
}