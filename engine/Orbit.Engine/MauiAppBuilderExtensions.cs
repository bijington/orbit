namespace Orbit.Engine;

/// <summary>
/// Extension methods for the <see cref="MauiAppBuilder"/>.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Initializes the Orbit game engine and any required dependencies.
    /// </summary>
    /// <param name="mauiAppBuilder">The <see cref="MauiAppBuilder"/> to register against.</param>
    /// <returns>The supplied <paramref name="mauiAppBuilder"/>.</returns>
    public static MauiAppBuilder UseOrbitEngine(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IGameSceneManager, GameSceneManager>();

        return mauiAppBuilder;
    }
}
