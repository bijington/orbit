using BuildingGames.GameObjects.Slidey;
using ChooseYourOwnAdventure;
using ChooseYourOwnAdventure.GameObjects;

namespace BuildingGames.GameObjects;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGameObjects(this IServiceCollection services) =>
        services
            .AddScoped<Ship>()
            .AddTransient<Bat>()
            .AddSingleton<Character>()
            .AddTransient<WorldMap>()
            .AddTransient<Pointer>();
}
