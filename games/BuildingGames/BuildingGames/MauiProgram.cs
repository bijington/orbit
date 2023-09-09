using BuildingGames.GameObjects;
using BuildingGames.Scenes;
using Orbit.Engine;

namespace BuildingGames;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
            .UseOrbitEngine()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("PublicPixel.ttf", "PublicPixel");
                fonts.AddFont("JosefinSans-Regular.ttf", "Josefin Sans");
                fonts.AddFont("JosefinSans-Bold.ttf", "Josefin Sans Bold");
                fonts.AddFont("CourierPrime-Regular.ttf", "Courier Prime");
            })
            .Services
                .AddTransient<MainPage>()
                .AddSingleton<ControllerManager>()
                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
	}
}
