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
                .AddSingleton(HapticFeedback.Default)
                .AddSingleton(Vibration.Default)
                .AddSingleton(FileSystem.Current)
                .AddSingleton(DeviceDisplay.Current)

                // Audio
                .AddSingleton(AudioManager.Current)

                // Internals
                .AddSingleton<UserInputManager>()
                .AddSingleton<AudioService>()

                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
    }
}
