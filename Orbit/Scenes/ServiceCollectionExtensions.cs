namespace Orbit.Scenes;

public static class ServiceCollectionExtensions
{
    public static void RegisterScenes(this IServiceCollection services)
    {
        services.AddTransient<HomeScene>();
        services.AddTransient<MainScene>();
    }
}
