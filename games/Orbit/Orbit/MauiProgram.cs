using Orbit.Audio;
using Orbit.Engine;
using Orbit.GameObjects;
using Orbit.Scenes;
using Plugin.Maui.Audio;

namespace Orbit;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseOrbitEngine() // Register the engine
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SpaceMono-Bold.ttf", "SpaceMonoBold");
            })
            .Services
                // Pages
                .AddTransient<MainPage>()

                // Essentials
                .AddSingleton(DeviceDisplay.Current)
                .AddSingleton(FileSystem.Current)
                .AddSingleton(HapticFeedback.Default)
                .AddSingleton(Vibration.Default)

                // Audio
                .AddSingleton(AudioManager.Current)

                // Internals
                .AddSingleton<AudioService>()
                .AddSingleton<SettingsManager>()
                .AddScoped<StatisticsManager>()
                .AddSingleton<UserInputManager>()

                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
    }
}
