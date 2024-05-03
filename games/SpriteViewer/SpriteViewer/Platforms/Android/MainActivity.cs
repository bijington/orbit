using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SpriteViewer;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges =
        ConfigChanges.ScreenSize |
        ConfigChanges.Orientation |
        ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout |
        ConfigChanges.SmallestScreenSize |
        ConfigChanges.Density,
    ScreenOrientation = ScreenOrientation.Landscape)]
public class MainActivity : MauiAppCompatActivity
{
}

