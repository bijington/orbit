using AirHockey.Server.Hubs;
using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server;

public class GameWorker : BackgroundService
{
    private readonly ILogger<GameWorker> _logger;
    private readonly GameManager gameManager;
    private readonly IHubContext<GameHub> _hubContext;

    public GameWorker(ILogger<GameWorker> logger, GameManager gameManager, IHubContext<GameHub> hubContext)
    {
        _logger = logger;
        this.gameManager = gameManager;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int delayInMilliseconds = 5;

            var game = this.gameManager.Games.FirstOrDefault();

            if (game is null)
            {
                this.gameManager.PlayGame(Guid.NewGuid());
                this.gameManager.PlayGame(Guid.NewGuid());

                game = this.gameManager.Games.FirstOrDefault();
            }

            // Perform movement
            game.PuckState.X += game.PuckState.VelocityX;
            game.PuckState.Y += game.PuckState.VelocityY;

            if (game.PuckState.Y > 1.0)
            {
                game.ScoreState.ScoreTwo++;
                game.PuckState.Y = 0.5;
                game.PuckState.VelocityY = -game.PuckState.VelocityY;

                await _hubContext.Clients.All.SendAsync(EventNames.ScoreUpdated, game.ScoreState);

                delayInMilliseconds = 5000;
            }
            else if (game.PuckState.Y < 0.0)
            {
                game.ScoreState.ScoreOne++;
                game.PuckState.Y = 0.5;
                game.PuckState.VelocityY = -game.PuckState.VelocityY;

                await _hubContext.Clients.All.SendAsync(EventNames.ScoreUpdated, game.ScoreState);

                delayInMilliseconds = 5000;
            }

            if (game.PuckState.X > 1.0)
            {
                game.PuckState.VelocityX = -game.PuckState.VelocityX;
            }
            else if (game.PuckState.X < 0.0)
            {
                game.PuckState.VelocityX = -game.PuckState.VelocityX;
            }

//.Group(game.Id.ToString())
            await _hubContext.Clients.All.SendAsync(EventNames.PuckStateUpdated, game.PuckState);

            // Check for paddle collision

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("GameWorker running at: {time} with: {playerOneId} and {playerOneId}", DateTimeOffset.Now, game.PlayerOne.Id, game.PlayerTwo.Id);

                var playerOne = game.PlayerOne;
                _logger.LogInformation("Player one at: {x},{y} with score: {score}", playerOne.X, playerOne.Y, game.ScoreState.ScoreOne);

                var playerTwo = game.PlayerTwo;
                _logger.LogInformation("Player two at: {x},{y} with score: {score}", playerTwo.X, playerTwo.Y, game.ScoreState.ScoreTwo);

                _logger.LogInformation("Puck at: {x},{y}", game.PuckState.X, game.PuckState.Y);
            }
            await Task.Delay(delayInMilliseconds, stoppingToken);
        }
    }
}