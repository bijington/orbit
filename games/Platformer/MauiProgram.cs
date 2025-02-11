using Microsoft.Extensions.Logging;
using Orbit.Engine;
using Orbit.Input;

using Platformer.GameObjects;
using Platformer.GameScenes;

namespace Platformer;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransient<MainPage>();
		builder.Services.RegisterGameObjects();
		builder.Services.RegisterScenes();

        builder.Services.AddSingleton<PlayerStateManager>();
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<Orbit.Input.GameController>();

		return builder.Build();
	}
}
