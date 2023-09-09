using BuildingGames.GameObjects.Slidey;

namespace BuildingGames.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services.AddScoped<Ship>();
}
