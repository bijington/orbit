using System.Diagnostics;
using Foundation;
using Orbit.Input;
using UIKit;

namespace Platformer;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    
    public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt) 
    {
        KeyboardManager.Current.PressesBegan(presses, evt);
        //base.PressesBegan(presses, evt);
    }
    
    public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        Trace.WriteLine(@"PressesCancelled");
        //KeyboardManager.Current.PressesCancelled(presses, evt);
        //base.PressesCancelled(presses, evt);
    }

    public override void PressesChanged(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        Trace.WriteLine(@"PressesChanged");
        //base.PressesChanged(presses, evt);
    }

    public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        KeyboardManager.Current.PressesEnded(presses, evt);
        //base.PressesEnded(presses, evt);
    }
}
