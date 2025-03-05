using Android.App;
using Android.Views;

using View = Android.Views.View;

namespace Orbit.Input;

public class GenericMotionListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalFocusChangeListener, View.IOnGenericMotionListener
{
    private readonly Func<MotionEvent, bool> callback;

    public GenericMotionListener(Activity activity, Func<MotionEvent, bool> callback)
    {
        this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        
        activity.Window?.DecorView.ViewTreeObserver?.AddOnGlobalFocusChangeListener(this);
    }

    public void OnGlobalFocusChanged(View? oldFocus, View? newFocus)
    {
        oldFocus?.SetOnGenericMotionListener(null);

        newFocus?.SetOnGenericMotionListener(this);
    }

    public bool OnGenericMotion(View? v, MotionEvent? e)
    {
        // Only return if something handles it
        return e is not null && callback.Invoke(e);
    }
}