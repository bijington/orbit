namespace DrawingGame.GameObjects;

public static class GameObjectServiceCollectionExtensions
{
	public static IServiceCollection AddGameObjects(this IServiceCollection services) =>
		services
			.AddTransient<ColorPalette>()
			.AddTransient<DrawingSurface>();
}
