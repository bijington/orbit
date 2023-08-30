namespace BuildingGames.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddTransient<Boarder>()
            .AddTransient<DistanceCounter>()
            .AddTransient<FreeBoarder>()
            .AddTransient<Log>()
            .AddScoped<StateObject>();
}
