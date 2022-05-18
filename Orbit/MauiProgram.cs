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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.UseGameEngine();
        builder.Services.AddTransient<MainPage>();

        builder.Services.RegisterGameObjects();
        builder.Services.RegisterScenes();

        return builder.Build();
    }
}
