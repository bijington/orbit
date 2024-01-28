using Microsoft.Extensions.Logging;

namespace AirHockey.Shared;

public class GameStateManager
{
    private readonly ILogger<GameStateManager> logger;
    private readonly IGameLifeCycleHandler gameLifeCycleHandler;

    public GameStateManager(ILogger<GameStateManager> logger, IGameLifeCycleHandler gameLifeCycleHandler)
    {
        this.logger = logger;
        this.gameLifeCycleHandler = gameLifeCycleHandler;
    }

    public async Task UpdateGame(Game game, double millisecondsSinceLastUpdate)
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

            await this.gameLifeCycleHandler.PuckCollision(game.Id, game.PlayerOne.Id);
        }
        else if (Physics.DoCirclesIntersect(game.PlayerTwo.X + radius, Math.Abs(game.PlayerTwo.Y - 1) + radius, radius, game.PuckState.X + puckRadius, game.PuckState.Y + puckRadius, puckRadius))
        {
            this.logger.LogInformation("Player two HIT!!!");
            // var angle = Physics.CalculateAngleAfterCollision(game.PlayerTwo, game.PuckState);
            // Physics.ApplyForceAfterCollision(game.PlayerTwo, game.PuckState, angle);
            // this.logger.LogInformation("Puck velocity: {x},{y}", game.PuckState.VelocityX, game.PuckState.VelocityY);
            game.PuckState.VelocityX = -game.PuckState.VelocityX;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.gameLifeCycleHandler.PuckCollision(game.Id, game.PlayerTwo.Id);
        }
        else if (game.PuckState.Y > 1.0)
        {
            game.ScoreState.ScoreTwo++;
            game.PuckState.Y = 0.5;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.gameLifeCycleHandler.ScoreUpdated(game.Id, game.ScoreState);

            delayInMilliseconds = 5000;
        }
        else if (game.PuckState.Y < 0.0)
        {
            game.ScoreState.ScoreOne++;
            game.PuckState.Y = 0.5;
            game.PuckState.VelocityY = -game.PuckState.VelocityY;

            await this.gameLifeCycleHandler.ScoreUpdated(game.Id, game.ScoreState);

            delayInMilliseconds = 5000;
        }

        if (game.PuckState.X > 1.0)
        {
            game.PuckState.VelocityX = -game.PuckState.VelocityX;

            await this.gameLifeCycleHandler.WallCollision(game.Id);
        }
        else if (game.PuckState.X < 0.0)
        {
            game.PuckState.VelocityX = -game.PuckState.VelocityX;

            await this.gameLifeCycleHandler.WallCollision(game.Id);
        }

        await this.gameLifeCycleHandler.PuckStateUpdated(game.Id, game.PuckState);
        //await this.hubContext.Clients.All.SendAsync(EventNames.PuckStateUpdated, game.PuckState);

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