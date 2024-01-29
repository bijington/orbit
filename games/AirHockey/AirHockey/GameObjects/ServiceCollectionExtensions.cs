namespace AirHockey.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddTransient<OpponentPaddle>()
            .AddScoped<GameManager>()
            .AddTransient<Paddle>()
            .AddTransient<Puck>()
            .AddTransient<ScoreDisplay>();
}
