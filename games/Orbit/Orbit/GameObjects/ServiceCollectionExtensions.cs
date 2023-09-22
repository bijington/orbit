namespace Orbit.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddTransient<Asteroid>()
            .AddTransient<AsteroidLauncher>()
            .AddTransient<AsteroidRemains>()
            .AddScoped<Battery>()
            .AddTransient<BatteryLevelIndicator>()
            .AddTransient<Gun>()
            .AddTransient<Pulse>()
            .AddScoped<Planet>()
            .AddTransient<PlanetHealthIndicator>()
            .AddTransient<Shadow>()
            .AddScoped<Ship>()
            .AddTransient<Sun>()
            .AddScoped<Thruster>();
}
