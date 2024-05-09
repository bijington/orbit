namespace Platformer.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddTransient<Cherries>()
            .AddTransient<CollectedEffect>()
            .AddTransient<FloorTile>()
            .AddTransient<PinkMan>();
}
