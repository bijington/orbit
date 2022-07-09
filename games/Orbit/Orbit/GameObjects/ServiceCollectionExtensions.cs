namespace Orbit.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddTransient<Asteroid>()
            .AddTransient<AsteroidLauncher>()
            .AddTransient<BatteryLevel>()
            .AddTransient<Planet>()
            .AddTransient<Shadow>()
            .AddTransient<Ship>()
            .AddTransient<Sun>();
}
