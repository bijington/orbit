namespace DrawingGame.Scenes;

public static class GameObjectServiceCollectionExtensions
{
    public static IServiceCollection AddScenes(this IServiceCollection services) =>
        services
            .AddTransient<MainScene>();
}
