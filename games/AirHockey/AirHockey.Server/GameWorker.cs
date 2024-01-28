using AirHockey.Server.Hubs;
using AirHockey.Shared;
using Microsoft.AspNetCore.SignalR;

namespace AirHockey.Server;

public class GameWorker : BackgroundService
{
    private readonly ILogger<GameWorker> logger;
    private readonly GameManager gameManager;
    private readonly IHubContext<GameHub> hubContext;

    public GameWorker(ILogger<GameWorker> logger, GameManager gameManager, IHubContext<GameHub> hubContext)
    {
        this.logger = logger;
        this.gameManager = gameManager;
        this.hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int delayInMilliseconds = 5;

            var game = this.gameManager.Games.FirstOrDefault();

            if (game is not null)
            {
                //this.logger.LogInformation("Found game to process {id}", game.Id);

                await ProcessGame(game);
            }

            await Task.Delay(delayInMilliseconds, stoppingToken);
        }
    }

    private async Task ProcessGame(AirHockey.Server.GameManager.Game game)
    {
        int delayInMilliseconds = 5;

        // Perform movement
        game.PuckState.X += game.PuckState.VelocityX;
        game.PuckState.Y += game.PuckState.VelocityY;

        var radius = game.PlayerOne.Size / 2;
        var puckRadius = game.PuckState.Size / 2;

        if (Physics.DoCirclesIntersect(game.PlayerOne.X + radius, game.PlayerOne.Y + radius, radius, game.PuckState.X + puckRadius, game.PuckState.Y + puckRadius, puckRadius))
        {
            this.logger.LogInformation("Player one HIT!!!");
            // var angle = Physics.CalculateAngleAfterCollision(game.PlayerOne, game.PuckState);
            // Physics.ApplyForceAfterCollision(game.PlayerOne, game.PuckState, angle);
            // this.logger.LogInformation("Puck velocity: {x},{y}", game.PuckState.VelocityX, game.PuckState.VelocityY);
            game.PuckState.VelocityX = -game.PuckState.VelocityX;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.hubContext.Clients.All.SendAsync(EventNames.PuckCollision, game.PlayerOne.Id);
        }
        else if (Physics.DoCirclesIntersect(game.PlayerTwo.X + radius, Math.Abs(game.PlayerTwo.Y - 1) + radius, radius, game.PuckState.X + puckRadius, game.PuckState.Y + puckRadius, puckRadius))
        {
            this.logger.LogInformation("Player two HIT!!!");
            // var angle = Physics.CalculateAngleAfterCollision(game.PlayerTwo, game.PuckState);
            // Physics.ApplyForceAfterCollision(game.PlayerTwo, game.PuckState, angle);
            // this.logger.LogInformation("Puck velocity: {x},{y}", game.PuckState.VelocityX, game.PuckState.VelocityY);
            game.PuckState.VelocityX = -game.PuckState.VelocityX;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.hubContext.Clients.All.SendAsync(EventNames.PuckCollision, game.PlayerTwo.Id);
        }
        else if (game.PuckState.Y > 1.0)
        {
            game.ScoreState.ScoreTwo++;
            game.PuckState.Y = 0.5;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.hubContext.Clients.All.SendAsync(EventNames.ScoreUpdated, game.ScoreState);

            delayInMilliseconds = 5000;
        }
        else if (game.PuckState.Y < 0.0)
        {
            game.ScoreState.ScoreOne++;
            game.PuckState.Y = 0.5;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.hubContext.Clients.All.SendAsync(EventNames.ScoreUpdated, game.ScoreState);

            delayInMilliseconds = 5000;
        }

        if (game.PuckState.X > 1.0)
        {
            game.PuckState.VelocityX = -game.PuckState.VelocityX;

            await this.hubContext.Clients.All.SendAsync(EventNames.WallCollision);
        }
        else if (game.PuckState.X < 0.0)
        {
            game.PuckState.VelocityX = -game.PuckState.VelocityX;

            await this.hubContext.Clients.All.SendAsync(EventNames.WallCollision);
        }

        //.Group(game.Id.ToString())
        await this.hubContext.Clients.All.SendAsync(EventNames.PuckStateUpdated, game.PuckState);

        // Check for paddle collision

        // if (this.logger.IsEnabled(LogLevel.Information))
        // {
        //     this.logger.LogInformation(
        //         "GameWorker running at: {time} with: {playerOneId} at {x},{y} and {playerOneId}",
        //         DateTimeOffset.Now,
        //         game.PlayerOne.Id,
        //         game.PlayerOne.X,
        //         game.PlayerOne.Y,
        //         game.PlayerTwo.Id);

        //     var playerOne = game.PlayerOne;
        //     this.logger.LogInformation("Player one at: {x},{y} with score: {score}", playerOne.X, playerOne.Y, game.ScoreState.ScoreOne);

        //     var playerTwo = game.PlayerTwo;
        //     this.logger.LogInformation("Player two at: {x},{y} with score: {score}", playerTwo.X, playerTwo.Y, game.ScoreState.ScoreTwo);

        //     this.logger.LogInformation("Puck at: {x},{y}", game.PuckState.X, game.PuckState.Y);
        // }
    }
}