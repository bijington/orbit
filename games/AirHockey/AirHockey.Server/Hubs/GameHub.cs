using AirHockey.Server;
using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server.Hubs;

public class GameHub : Hub
{
    public GameHub(ILogger<GameWorker> logger, GameManager gameManager)
    {
        this.logger = logger;
        this.gameManager = gameManager;
    }

    private readonly ILogger<GameWorker> logger;
    private readonly GameManager gameManager;

    public async Task PlayGame(Guid playerId)
    {
        var game = this.gameManager.PlayGame(playerId);

        var connectedPlayer = game.PlayerOne.Id == playerId ? game.PlayerOne : game.PlayerTwo;

        this.logger.LogInformation("Player connected {id}", playerId);

        await Clients.All.SendAsync(EventNames.PlayerConnected, connectedPlayer);

        if (game.PlayerTwo != PlayerState.Empty)
        {
            await Clients.All.SendAsync(EventNames.GameStarted, new GameState(game.Id, game.PlayerOne, game.PlayerTwo));
        }
    }

    // TODO: Disconnected.

    public async Task UpdatePlayerState(PlayerState playerState)
    {
        var game = this.gameManager.Games.FirstOrDefault(g => g.PlayerOne.Id == playerState.Id || g.PlayerTwo.Id == playerState.Id);

        if (game is not null)
        {
            this.logger.LogInformation("Update player {id}", playerState.Id);

            if (game.PlayerOne.Id == playerState.Id)
            {
                game.PlayerOne = playerState;
            }
            else
            {
                game.PlayerTwo = playerState;
            }

            await Clients.Others.SendAsync(EventNames.PlayerStateUpdated, playerState);
        }
    }
}
