using AirHockey.Server;
using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server.Hubs;

public class GameHub : Hub
{
    public GameHub(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private readonly GameManager gameManager;

    public async Task PlayGame(Guid playerId)
    {
        var game = this.gameManager.PlayGame(playerId);

        var connectedPlayer = game.PlayerOne.Id == playerId ? game.PlayerOne : game.PlayerTwo;

        await Clients.All.SendAsync(EventNames.PlayerConnected, connectedPlayer);

        if (game.PlayerTwo != PlayerState.Empty)
        {
            await Clients.All.SendAsync(EventNames.GameStarted);
        }
    }

    public async Task UpdatePlayerState(PlayerState playerState)
    {
        var game = this.gameManager.Games.FirstOrDefault(g => g.PlayerOne.Id == playerState.Id || g.PlayerTwo.Id == playerState.Id);

        if (game is not null)
        {
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
