using BuildingGames.GameObjects.Slidey;
using ChooseYourOwnAdventure;

namespace BuildingGames.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddScoped<Ship>()
            .AddTransient<WorldMap>()
            .AddTransient<Pointer>();
}
