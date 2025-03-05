using Android.InputMethodServices;
using Android.Views;
using Activity = Android.App.Activity;
using Keycode = Android.Views.Keycode;
using View = Android.Views.View;

namespace Orbit.Input;

public partial class KeyboardManager
{
    private KeyListener? listener;
    
    private static bool IsKeyboardDevice(InputEvent inputEvent)
    {
        // Check that input comes from a device with directional pads.
        return inputEvent.Source.HasFlag(InputSourceType.Keyboard);
    }

    public void AttachToCurrentActivity(Activity activity) 
    {
        listener = new(activity, (code, e) =>
        {
            if (!IsKeyboardDevice(e))
            {
                return false;
            }
            
            var mapped = MapToMaui(code);

            switch (e)
            {
                case { Action: KeyEventActions.Down }:
                    KeyboardKeyPressed(mapped);
                    break;
                case { Action: KeyEventActions.Up }:
                    KeyboardKeyReleased(mapped);
                    break;
            }

            return true;
        });
    }

    public static KeyboardKey MapToMaui(Keycode keycode)
    {
        switch (keycode)
        {
            case Keycode.Space: return KeyboardKey.Space;
            case Keycode.DpadLeft: return KeyboardKey.ArrowLeft;
            case Keycode.DpadUp: return KeyboardKey.ArrowUp;
            case Keycode.DpadRight: return KeyboardKey.ArrowRight;
            case Keycode.DpadDown: return KeyboardKey.ArrowDown;

            case Keycode.Num0: return KeyboardKey.Digit0;
            case Keycode.Num1: return KeyboardKey.Digit1;
            case Keycode.Num2: return KeyboardKey.Digit2;
            case Keycode.Num3: return KeyboardKey.Digit3;
            case Keycode.Num4: return KeyboardKey.Digit4;
            case Keycode.Num5: return KeyboardKey.Digit5;
            case Keycode.Num6: return KeyboardKey.Digit6;
            case Keycode.Num7: return KeyboardKey.Digit7;
            case Keycode.Num8: return KeyboardKey.Digit8;
            case Keycode.Num9: return KeyboardKey.Digit9;

            case Keycode.A: return KeyboardKey.KeyA;
            case Keycode.B: return KeyboardKey.KeyB;
            case Keycode.C: return KeyboardKey.KeyC;
            case Keycode.D: return KeyboardKey.KeyD;
            case Keycode.E: return KeyboardKey.KeyE;
            case Keycode.F: return KeyboardKey.KeyF;
            case Keycode.G: return KeyboardKey.KeyG;
            case Keycode.H: return KeyboardKey.KeyH;
            case Keycode.I: return KeyboardKey.KeyI;
            case Keycode.J: return KeyboardKey.KeyJ;
            case Keycode.K: return KeyboardKey.KeyK;
            case Keycode.L: return KeyboardKey.KeyL;
            case Keycode.M: return KeyboardKey.KeyM;
            case Keycode.N: return KeyboardKey.KeyN;
            case Keycode.O: return KeyboardKey.KeyO;
            case Keycode.P: return KeyboardKey.KeyP;
            case Keycode.Q: return KeyboardKey.KeyQ;
            case Keycode.R: return KeyboardKey.KeyR;
            case Keycode.S: return KeyboardKey.KeyS;
            case Keycode.T: return KeyboardKey.KeyT;
            case Keycode.U: return KeyboardKey.KeyU;
            case Keycode.V: return KeyboardKey.KeyV;
            case Keycode.W: return KeyboardKey.KeyW;
            case Keycode.X: return KeyboardKey.KeyX;
            case Keycode.Y: return KeyboardKey.KeyY;
            case Keycode.Z: return KeyboardKey.KeyZ;

            case Keycode.CapsLock: return KeyboardKey.CapsLock;
            case Keycode.Insert: return KeyboardKey.Insert;
            case Keycode.Del: return KeyboardKey.Delete;
            // Android doesn�t have a dedicated Print Screen key in most cases.
            case Keycode.Home: return KeyboardKey.Home;
            case Keycode.MoveEnd: return KeyboardKey.End;
            case Keycode.PageDown: return KeyboardKey.PageDown;
            case Keycode.PageUp: return KeyboardKey.PageUp;
            case Keycode.Escape: return KeyboardKey.Escape;
            case Keycode.MediaPause: return KeyboardKey.Pause;

            case Keycode.Menu: return KeyboardKey.AltLeft; // Often maps to the Alt key
            case Keycode.ShiftLeft: return KeyboardKey.ShiftLeft;
            case Keycode.ShiftRight: return KeyboardKey.ShiftRight;
            case Keycode.CtrlLeft: return KeyboardKey.ControlLeft;
            case Keycode.CtrlRight: return KeyboardKey.ControlRight;
            case Keycode.Enter: return KeyboardKey.Enter;
            case Keycode.Tab: return KeyboardKey.Tab;
            case Keycode.Back: return KeyboardKey.Backspace;

            case Keycode.F1: return KeyboardKey.F1;
            case Keycode.F2: return KeyboardKey.F2;
            case Keycode.F3: return KeyboardKey.F3;
            case Keycode.F4: return KeyboardKey.F4;
            case Keycode.F5: return KeyboardKey.F5;
            case Keycode.F6: return KeyboardKey.F6;
            case Keycode.F7: return KeyboardKey.F7;
            case Keycode.F8: return KeyboardKey.F8;
            case Keycode.F9: return KeyboardKey.F9;
            case Keycode.F10: return KeyboardKey.F10;
            case Keycode.F11: return KeyboardKey.F11;
            case Keycode.F12: return KeyboardKey.F12;

            case Keycode.NumLock: return KeyboardKey.NumLock;
            case Keycode.ScrollLock: return KeyboardKey.ScrollLock;

            case Keycode.MetaLeft: return KeyboardKey.MetaLeft;
            case Keycode.MetaRight: return KeyboardKey.MetaRight;
            case Keycode.NumpadDivide: return KeyboardKey.NumpadDivide;
            case Keycode.NumpadMultiply: return KeyboardKey.NumpadMultiply;
            case Keycode.NumpadSubtract: return KeyboardKey.NumpadSubtract;
            case Keycode.NumpadAdd: return KeyboardKey.NumpadAdd;

            // Punctuation and symbol keys
            case Keycode.Equals: return KeyboardKey.Equal;
            case Keycode.Minus: return KeyboardKey.Minus;
            case Keycode.Grave: return KeyboardKey.Backquote;
            case Keycode.Comma: return KeyboardKey.Comma;
            case Keycode.Period: return KeyboardKey.Period;
            case Keycode.Slash: return KeyboardKey.Slash;
            case Keycode.LeftBracket: return KeyboardKey.BracketLeft;
            case Keycode.RightBracket: return KeyboardKey.BracketRight;
            case Keycode.Backslash: return KeyboardKey.Backslash;
            case Keycode.Semicolon: return KeyboardKey.Semicolon;

            default:
                return KeyboardKey.Unknown;
        }
    }
}