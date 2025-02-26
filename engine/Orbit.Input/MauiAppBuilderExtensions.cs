using Microsoft.Maui.LifecycleEvents;

namespace Orbit.Input;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseOrbitInput(
        this MauiAppBuilder builder,
        Action<GameControllerOptions>? configureControllerOptions = null,
        Action<KeyboardOptions>? configureKeyboardOptions = null) =>
        builder
            .UseOrbitGameController(configureControllerOptions)
            .UseOrbitKeyboard(configureKeyboardOptions);
    
    public static MauiAppBuilder UseOrbitGameController(
        this MauiAppBuilder builder,
        Action<GameControllerOptions>? configureControllerOptions)
    {
        var controllerOptions = new GameControllerOptions();
        configureControllerOptions?.Invoke(controllerOptions);
        
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
            appLifecycle.AddAndroid((android) =>
            {
                bool appCreated = false;

                android.OnCreate((activity, bundle) =>
                {
                    if (!appCreated)
                    {                        
                        appCreated = true;
                        
                        if (controllerOptions.AutoAttachToLifecycleEvents)
                        {
                            GameControllerManager.Current.AttachToCurrentActivity(activity);
                        }
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
            appLifecycle.AddAndroid((android) =>
            {
                bool appCreated = false;

                android.OnCreate((activity, bundle) =>
                {
                    if (!appCreated)
                    {                        
                        appCreated = true;

                        if (keyboardOptions.AutoAttachToLifecycleEvents)
                        {
                            KeyboardManager.Current.AttachToCurrentActivity(activity);
                        }
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