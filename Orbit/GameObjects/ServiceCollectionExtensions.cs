namespace Orbit.GameObjects;

public static class ServiceCollectionExtensions
{
    public static void RegisterGameObjects(this IServiceCollection services)
    {
        services.AddTransient<Asteroid>();
        services.AddTransient<BatteryLevel>();
        services.AddTransient<Planet>();
        services.AddTransient<Shadow>();
        services.AddTransient<Ship>();
        services.AddTransient<Sun>();
    }
}
