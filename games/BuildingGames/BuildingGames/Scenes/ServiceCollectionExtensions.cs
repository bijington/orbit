namespace BuildingGames.Scenes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterScenes(this IServiceCollection services) =>
        services
            .AddTransient<CharacterSelectionScene>()
            .AddTransient<CreditsScene>()
            .AddTransient<FreeMainScene>()
            .AddTransient<GameControllerScene>()
            .AddTransient<GameControllerPartTwoScene>()
            .AddTransient<GraphicsScene>()
            .AddTransient<HandlingInputScene>()
            .AddTransient<MainScene>()
            .AddTransient<TitleScene>();
}
