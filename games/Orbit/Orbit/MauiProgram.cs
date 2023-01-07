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
            .UseOrbitEngine()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services
                .AddTransient<MainPage>()
                .AddSingleton(HapticFeedback.Default)
                .AddSingleton(Vibration.Default)
                .AddSingleton(FileSystem.Current)
                .AddSingleton(AudioManager.Current)
                .AddSingleton<AudioService>()
                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
    }
}
