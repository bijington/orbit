using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace AirHockey;

public class PlayerStateManager
{
    private HubConnection hubConnection;

    public PlayerState PlayerState { get; private set; }

    public PlayerState OpponentState { get; private set; }

    public PuckState PuckState { get; private set; }

    public ScoreState ScoreState { get; private set; }

    public PlayerStateManager()
	{
        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7226/Game")
            .Build();

        PuckState = new();
        ScoreState = new();
        PlayerState = new(Guid.NewGuid());

        hubConnection.On<PuckState>(EventNames.PuckStateUpdated, puckState =>
        {
            PuckState = puckState;
        });
        hubConnection.On<ScoreState>(EventNames.ScoreUpdated, scoreState =>
        {
            ScoreState = scoreState;
        });

        hubConnection.On<PlayerState>(EventNames.PlayerStateUpdated, playerState =>
        {
            OpponentState = playerState;
        });

        hubConnection.On<PlayerState>(EventNames.PlayerConnected, playerState =>
        {
            if (PlayerState.Id == playerState.Id)
            {
                PlayerState = playerState;
            }
            else
            {
                OpponentState = playerState;
            }
        });

        hubConnection.On<GameState>(EventNames.GameStarted, gameState =>
        {
            if (PlayerState.Id == gameState.PlayerOne.Id)
            {
                PlayerState = gameState.PlayerOne;
                OpponentState = gameState.PlayerTwo;
            }
            else
            {
                PlayerState = gameState.PlayerTwo;
                OpponentState = gameState.PlayerOne;
            }
        });
    }

    public async Task Connect()
    {
        await hubConnection.StartAsync();

        await hubConnection.SendAsync(MethodNames.PlayGame, PlayerState.Id);
    }

    public Task Disconnect()
    {
        return hubConnection.StopAsync();
    }

    public async Task UpdateState(float x, float y)
    {
        PlayerState.X = x;
        PlayerState.Y = y;
        await hubConnection.SendAsync(MethodNames.UpdatePlayerState, PlayerState);
    }
}
