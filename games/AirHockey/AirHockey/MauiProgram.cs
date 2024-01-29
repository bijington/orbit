using AirHockey.GameObjects;
using AirHockey.Scenes;
using AirHockey.Shared;
using Orbit.Engine;
using Plugin.Maui.Audio;

namespace AirHockey;

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
            })
            .Services
                .AddTransient<MainPage>()
                .AddSingleton<PlayerStateManager>()
                .AddSingleton<IGameLifeCycleHandler>(x => x.GetRequiredService<PlayerStateManager>())
                .AddSingleton<GameSceneManager>()
                .AddSingleton<GameStateManager>()
                .AddSingleton(HapticFeedback.Default)
                .AddSingleton(Vibration.Default)
                .AddSingleton(AudioManager.Current)
                .AddSingleton(FileSystem.Current)
                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
	}
}
