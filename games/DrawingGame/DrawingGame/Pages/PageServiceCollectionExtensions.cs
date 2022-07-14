namespace DrawingGame.Pages;

public static class PageServiceCollectionExtensions
{
    public static IServiceCollection AddPages(this IServiceCollection services) =>
        services
            .AddTransient<MainPage>();
}
