using Orbit.Engine;
using Orbit.GameObjects;
using Orbit.Scenes;

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
                .RegisterGameObjects()
                .RegisterScenes();

        return builder.Build();
    }
}
