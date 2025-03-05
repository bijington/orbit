using Android.App;
using Android.Views;

using View = Android.Views.View;

namespace Orbit.Input;

public class KeyListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalFocusChangeListener, View.IOnKeyListener
{
    private readonly Func<Keycode, KeyEvent, bool> callback;

    public KeyListener(Activity activity, Func<Keycode, KeyEvent, bool> callback)
    {
        this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        
        activity.Window?.DecorView.ViewTreeObserver?.AddOnGlobalFocusChangeListener(this);
    }

    public void OnGlobalFocusChanged(View? oldFocus, View? newFocus)
    {
        oldFocus?.SetOnKeyListener(null);

        newFocus?.SetOnKeyListener(this);
    }

    /// <summary>
    /// You have to return `true` if the key was handled. We will return `true` always in this implementation.
    /// </summary>
    /// <param name="v"></param>
    /// <param name="keyCode"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool OnKey(View? v, Keycode keyCode, KeyEvent? e)
    {
        // Only return if something handles it
        return e is not null && callback.Invoke(keyCode, e);
    }
}