namespace BuildingGames.Slides;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterScenes(this IServiceCollection services)
    {
        foreach (var slideType in SlideDeck.Slides)
        {
            services.AddTransient(slideType);

            if (slideType.IsAssignableTo(typeof(ContentPage)))
            {
                Routing.RegisterRoute(slideType.Name, slideType);
            }
        }

        return services;
    }
}
