namespace Orbit.Engine;

public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Registers to use the Orbit game engine.
    /// </summary>
    /// <param name="mauiAppBuilder">The <see cref="MauiAppBuilder"/> to register against.</param>
    /// <returns>The supplied <paramref name="mauiAppBuilder"/>.</returns>
    public static MauiAppBuilder UseOrbitEngine(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IGameSceneManager, GameSceneManager>();

        return mauiAppBuilder;
    }
}
