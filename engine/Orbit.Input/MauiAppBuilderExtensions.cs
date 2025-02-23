using Microsoft.Maui.LifecycleEvents;

namespace Orbit.Input;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseOrbitInput(
        this MauiAppBuilder builder,
        Action<GameControllerOptions>? configureControllerOptions = null)
    {
        var controllerOptions = new GameControllerOptions();
        configureControllerOptions?.Invoke(controllerOptions);

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
                        
                        // TODO: Do the same for keyboard later
                        // do we need to do anything to allow them to play nicely together?
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
}