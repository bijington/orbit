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
                fonts.AddFont("JosefinSans-VariableFont_wght.ttf", "Josefin");
            })
            .Services
                .AddTransient<MainPage>()
                .AddSingleton<ControllerManager>()
                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
	}
}
