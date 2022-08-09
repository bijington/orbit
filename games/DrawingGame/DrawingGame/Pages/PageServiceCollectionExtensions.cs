namespace DrawingGame.Pages;

public static class PageServiceCollectionExtensions
{
    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        Routing.RegisterRoute("join", typeof(JoinGamePage));
        Routing.RegisterRoute("lobby", typeof(LobbyPage));
        Routing.RegisterRoute("main", typeof(MainPage));

        return services
            .AddTransient<JoinGamePage>()
            .AddTransient<LobbyPage>()
            .AddTransient<MainPage>()
            .AddTransient<StartPage>();
    }
}
