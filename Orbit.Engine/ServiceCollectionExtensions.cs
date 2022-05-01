namespace Orbit.Engine;

public static class ServiceCollectionExtensions
{
	public static void UseGameEngine(this IServiceCollection services)
    {
        services.AddSingleton<IGameSceneManager, GameSceneManager>();
    }
}
