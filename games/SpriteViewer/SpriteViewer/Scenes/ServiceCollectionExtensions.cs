namespace Orbit.Scenes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterScenes(this IServiceCollection services) =>
        services
            .AddTransient<SpriteViewerScene>();
}
