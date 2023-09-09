namespace BuildingGames.Scenes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterScenes(this IServiceCollection services) =>
        services
            .AddTransient<Slide01>()
            .AddTransient<Slide02>()
            .AddTransient<Slide03>()
            .AddTransient<Slide04>();
}
