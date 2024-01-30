using AirHockey.Server.Hubs;
using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server;

public class GameWorker : BackgroundService
{
    private readonly ILogger<GameWorker> logger;
    private readonly GameManager gameManager;
    private readonly GameStateManager gameStateManager;

    public GameWorker(ILogger<GameWorker> logger, GameManager gameManager, GameStateManager gameStateManager)
    {
        this.logger = logger;
        this.gameManager = gameManager;
        this.gameStateManager = gameStateManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int delayInMilliseconds = 16;

            var game = this.gameManager.Games.FirstOrDefault();

            if (game is not null)
            {
                await this.gameStateManager.UpdateGame(game, delayInMilliseconds);
            }

            await Task.Delay(delayInMilliseconds, stoppingToken);
        }
    }
}