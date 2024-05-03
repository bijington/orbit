using Orbit.Engine;
using Orbit.Scenes;

namespace SpriteViewer;

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
                .RegisterScenes();

        return builder.Build();
    }
}
