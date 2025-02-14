using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Platformer;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public override bool OnGenericMotionEvent(MotionEvent? e)
    {
        GameControllerManager.Current.OnGenericMotionEvent(e);
        
        return base.OnGenericMotionEvent(e);
    }

    public override bool OnKeyDown(Keycode keyCode, KeyEvent? e)
    {
        GameControllerManager.Current.OnKeyDown(e);
        
        return base.OnKeyDown(keyCode, e);
    }

    public override bool OnKeyUp(Keycode keyCode, KeyEvent? e)
    {
        GameControllerManager.Current.OnKeyUp(e);
        
        return base.OnKeyUp(keyCode, e);
    }
}
