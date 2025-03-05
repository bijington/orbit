using Microsoft.Maui.LifecycleEvents;

namespace Orbit.Input;

/// <summary>
/// Extensions for the <see cref="MauiAppBuilder"/>.
/// </summary>
public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseOrbitInput(
        this MauiAppBuilder builder,
        Action<GameControllerOptions>? configureControllerOptions = null,
        Action<KeyboardOptions>? configureKeyboardOptions = null) =>
        builder
            .UseOrbitGameController(configureControllerOptions)
            .UseOrbitKeyboard(configureKeyboardOptions);
    
    /// <summary>
    /// Initializes the game controller libraries to be integrated with the current application.
    /// </summary>
    /// <param name="builder">The <see cref="MauiAppBuilder"/> to register the package with.</param>
    /// <param name="configureControllerOptions">
    /// The mechanism to define any options to customize the game controller integration. Note this is optional.
    /// An example of configuring the integration to not auto attach on Android is as follows:
    /// <code>
    /// builder
    ///     .UseOrbitGameController(
    ///			configureControllerOptions: options =>
    ///			{
    ///#if ANDROID
    ///				options.AutoAttachToLifecycleEvents = false;
    ///#endif
    ///			});
    /// </code>
    /// </param>
    /// <returns>The <paramref name="builder"/> supplied in order to allow for chaining of method calls.</returns>
    public static MauiAppBuilder UseOrbitGameController(
        this MauiAppBuilder builder,
        Action<GameControllerOptions>? configureControllerOptions)
    {
        var controllerOptions = new GameControllerOptions();
        configureControllerOptions?.Invoke(controllerOptions);

        FloatComparer.Default = new FloatComparer(controllerOptions.ComparisonThreshold);
        
//         builder.ConfigureMauiHandlers(handlers =>
//         {
// #if IOS || MACCATALYST
//             PageHandler.PlatformViewFactory = (handler) =>
//             {
//                 var vc = new KeyboardPageViewController(handler.VirtualView, handler.MauiContext!);
//                 handler.ViewController = vc;
//                 return (Microsoft.Maui.Platform.ContentView)vc.View!.Subviews[0];
//             };
// #endif
//         });
        
        builder.ConfigureLifecycleEvents(appLifecycle =>
        {
#if ANDROID
            appLifecycle.AddAndroid(android =>
            {
                bool appCreated = false;

                android.OnCreate((activity, bundle) =>
                {
                    if (appCreated)
                    {
                        return;
                    }

                    appCreated = true;
                        
                    if (controllerOptions.AutoAttachToLifecycleEvents)
                    {
                        GameControllerManager.Current.AttachToCurrentActivity(activity);
                    }
                });
            });
#endif
        });

#if WINDOWS
        GameControllerManager.Current.StartControllerMonitoringUponDetection = controllerOptions.StartControllerMonitoringUponDetection;
        GameControllerManager.Current.ControllerUpdateFrequency = controllerOptions.ControllerUpdateFrequency;
#endif

        return builder;
    }
    
    /// <summary>
    /// Initializes the keyboard libraries to be integrated with the current application.
    /// </summary>
    /// <param name="builder">The <see cref="MauiAppBuilder"/> to register the package with.</param>
    /// <param name="configureKeyboardOptions">
    /// The mechanism to define any options to customize the keyboard integration. Note this is optional.
    /// An example of configuring the integration to not auto attach on Android is as follows:
    /// <code>
    /// builder
    ///     .UseOrbitGameController(
    ///			configureKeyboardOptions: options =>
    ///			{
    ///#if ANDROID
    ///				options.AutoAttachToLifecycleEvents = false;
    ///#endif
    ///			});
    /// </code>
    /// </param>
    /// <returns>The <paramref name="builder"/> supplied in order to allow for chaining of method calls.</returns>
    public static MauiAppBuilder UseOrbitKeyboard(
        this MauiAppBuilder builder,
        Action<KeyboardOptions>? configureKeyboardOptions = null)
    {
        var keyboardOptions = new KeyboardOptions();
        configureKeyboardOptions?.Invoke(keyboardOptions);
        
//         builder.ConfigureMauiHandlers(handlers =>
//         {
// #if IOS || MACCATALYST
//             PageHandler.PlatformViewFactory = (handler) =>
//             {
//                 var vc = new KeyboardPageViewController(handler.VirtualView, handler.MauiContext!);
//                 handler.ViewController = vc;
//                 return (Microsoft.Maui.Platform.ContentView)vc.View!.Subviews[0];
//             };
// #endif
//         });
        
        builder.ConfigureLifecycleEvents(appLifecycle =>
        {
#if ANDROID
            appLifecycle.AddAndroid(android =>
            {
                bool appCreated = false;

                android.OnCreate((activity, _) =>
                {
                    if (appCreated)
                    {
                        return;
                    }

                    appCreated = true;

                    if (keyboardOptions.AutoAttachToLifecycleEvents)
                    {
                        KeyboardManager.Current.AttachToCurrentActivity(activity);
                    }
                });
            });
#elif WINDOWS
            appLifecycle.AddEvent<WindowsLifecycle.OnLaunched>(
                "OnLaunched",
                (Microsoft.UI.Xaml.Application application, Microsoft.UI.Xaml.LaunchActivatedEventArgs args) =>
                {
                    var appWindow = Application.Current?.Windows.First();

                    if (appWindow is null)
                    {
                        return;
                    }

                    var window = appWindow.Handler?.PlatformView as Microsoft.Maui.MauiWinUIWindow;

                    if (window is not null)
                    {
                        KeyboardManager.Current.AttachKeyboard(window.Content);
                    }
                });
#endif
        });

        return builder;
    }
}