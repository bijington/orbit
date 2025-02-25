using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace Orbit.Input;

public class KeyboardPageViewController : PageViewController
{
    internal KeyboardPageViewController(IView page, IMauiContext mauiContext) 
        : base(page, mauiContext)
    {
    }
    
    public override void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt) 
    {
        KeyboardManager.Current.PressesBegan(presses, evt);
        base.PressesBegan(presses, evt);
    }
    
    // public override void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
    // {
    //     KeyboardManager.Current.PressesCancelled(presses, evt);
    //     base.PressesCancelled(presses, evt);
    // }

    public override void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        KeyboardManager.Current.PressesEnded(presses, evt);
        base.PressesEnded(presses, evt);
    }
}
