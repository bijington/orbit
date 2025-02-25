using Windows.System;
using Microsoft.UI.Xaml;

namespace Orbit.Input;

public partial class KeyboardManager
{
    public static void AttachKeyboard(UIElement window)
    {
        window.PreviewKeyUp += (sender, args) =>
        {
            var mapped = MapToMaui(args.Key);
            KeyboardReleased(mapped);

            //Trace.WriteLine($"[KEY] {args.Key} => {mapped}");
        };
        window.PreviewKeyDown += (sender, args) =>
        {
            var mapped = MapToMaui(args.Key);
            KeyboardPressed(mapped);
        };
    }

    /// <summary>
    /// Same as map to Java tbh
    /// </summary>
    /// <param name="virtualKey"></param>
    /// <returns></returns>
    public static KeyboardKey MapToMaui(VirtualKey virtualKey)
    {
        switch (virtualKey)
        {
        case VirtualKey.Space: return KeyboardKey.Space;
        case VirtualKey.Left: return KeyboardKey.ArrowLeft;
        case VirtualKey.Up: return KeyboardKey.ArrowUp;
        case VirtualKey.Right: return KeyboardKey.ArrowRight;
        case VirtualKey.Down: return KeyboardKey.ArrowDown;
        case VirtualKey.Number0: return KeyboardKey.Digit0;
        case VirtualKey.Number1: return KeyboardKey.Digit1;
        case VirtualKey.Number2: return KeyboardKey.Digit2;
        case VirtualKey.Number3: return KeyboardKey.Digit3;
        case VirtualKey.Number4: return KeyboardKey.Digit4;
        case VirtualKey.Number5: return KeyboardKey.Digit5;
        case VirtualKey.Number6: return KeyboardKey.Digit6;
        case VirtualKey.Number7: return KeyboardKey.Digit7;
        case VirtualKey.Number8: return KeyboardKey.Digit8;
        case VirtualKey.Number9: return KeyboardKey.Digit9;
        case VirtualKey.A: return KeyboardKey.KeyA;
        case VirtualKey.B: return KeyboardKey.KeyB;
        case VirtualKey.C: return KeyboardKey.KeyC;
        case VirtualKey.D: return KeyboardKey.KeyD;
        case VirtualKey.E: return KeyboardKey.KeyE;
        case VirtualKey.F: return KeyboardKey.KeyF;
        case VirtualKey.G: return KeyboardKey.KeyG;
        case VirtualKey.H: return KeyboardKey.KeyH;
        case VirtualKey.I: return KeyboardKey.KeyI;
        case VirtualKey.J: return KeyboardKey.KeyJ;
        case VirtualKey.K: return KeyboardKey.KeyK;
        case VirtualKey.L: return KeyboardKey.KeyL;
        case VirtualKey.M: return KeyboardKey.KeyM;
        case VirtualKey.N: return KeyboardKey.KeyN;
        case VirtualKey.O: return KeyboardKey.KeyO;
        case VirtualKey.P: return KeyboardKey.KeyP;
        case VirtualKey.Q: return KeyboardKey.KeyQ;
        case VirtualKey.R: return KeyboardKey.KeyR;
        case VirtualKey.S: return KeyboardKey.KeyS;
        case VirtualKey.T: return KeyboardKey.KeyT;
        case VirtualKey.U: return KeyboardKey.KeyU;
        case VirtualKey.V: return KeyboardKey.KeyV;
        case VirtualKey.W: return KeyboardKey.KeyW;
        case VirtualKey.X: return KeyboardKey.KeyX;
        case VirtualKey.Y: return KeyboardKey.KeyY;
        case VirtualKey.Z: return KeyboardKey.KeyZ;
        case VirtualKey.CapitalLock: return KeyboardKey.CapsLock;
        case VirtualKey.Insert: return KeyboardKey.Insert;
        case VirtualKey.Delete: return KeyboardKey.Delete;
        case VirtualKey.Snapshot: return KeyboardKey.PrintScreen;
        case VirtualKey.Home: return KeyboardKey.Home;
        case VirtualKey.End: return KeyboardKey.End;
        case VirtualKey.PageDown: return KeyboardKey.PageDown;
        case VirtualKey.PageUp: return KeyboardKey.PageUp;
        case VirtualKey.Escape: return KeyboardKey.Escape;
        case VirtualKey.Pause: return KeyboardKey.Pause;
        case VirtualKey.Menu: return KeyboardKey.AltLeft;
        case VirtualKey.LeftMenu: return KeyboardKey.AltLeft;
        case VirtualKey.RightMenu: return KeyboardKey.AltRight;
        case VirtualKey.Shift: return KeyboardKey.ShiftLeft;
        case VirtualKey.LeftShift: return KeyboardKey.ShiftLeft;
        case VirtualKey.RightShift: return KeyboardKey.ShiftRight;
        case VirtualKey.LeftControl: return KeyboardKey.ControlLeft;
        case VirtualKey.RightControl: return KeyboardKey.ControlRight;
        case VirtualKey.Control: return KeyboardKey.ControlLeft;
        case VirtualKey.Enter: return KeyboardKey.Enter;
        case VirtualKey.Tab: return KeyboardKey.Tab;
        case VirtualKey.Back: return KeyboardKey.Backspace;
        case VirtualKey.F1: return KeyboardKey.F1;
        case VirtualKey.F2: return KeyboardKey.F2;
        case VirtualKey.F3: return KeyboardKey.F3;
        case VirtualKey.F4: return KeyboardKey.F4;
        case VirtualKey.F5: return KeyboardKey.F5;
        case VirtualKey.F6: return KeyboardKey.F6;
        case VirtualKey.F7: return KeyboardKey.F7;
        case VirtualKey.F8: return KeyboardKey.F8;
        case VirtualKey.F9: return KeyboardKey.F9;
        case VirtualKey.F10: return KeyboardKey.F10;
        case VirtualKey.F11: return KeyboardKey.F11;
        case VirtualKey.F12: return KeyboardKey.F12;
        case VirtualKey.NumberKeyLock: return KeyboardKey.NumLock;
        case VirtualKey.Scroll: return KeyboardKey.ScrollLock;
        case VirtualKey.NumberPad0: return KeyboardKey.Numpad0;
        case VirtualKey.NumberPad1: return KeyboardKey.Numpad1;
        case VirtualKey.NumberPad2: return KeyboardKey.Numpad2;
        case VirtualKey.NumberPad3: return KeyboardKey.Numpad3;
        case VirtualKey.NumberPad4: return KeyboardKey.Numpad4;
        case VirtualKey.NumberPad5: return KeyboardKey.Numpad5;
        case VirtualKey.NumberPad6: return KeyboardKey.Numpad6;
        case VirtualKey.NumberPad7: return KeyboardKey.Numpad7;
        case VirtualKey.NumberPad8: return KeyboardKey.Numpad8;
        case VirtualKey.NumberPad9: return KeyboardKey.Numpad9;
        case VirtualKey.LeftWindows: return KeyboardKey.MetaLeft;
        case VirtualKey.RightWindows: return KeyboardKey.MetaRight;
        case VirtualKey.Divide: return KeyboardKey.NumpadDivide;
        case VirtualKey.Multiply: return KeyboardKey.NumpadMultiply;
        case VirtualKey.Subtract: return KeyboardKey.NumpadSubtract;
        case VirtualKey.Add: return KeyboardKey.NumpadAdd;
        }

        switch ((int)virtualKey)
        {
        case 187: return KeyboardKey.Equal;
        case 189: return KeyboardKey.Minus;
        case 192: return KeyboardKey.Backquote;
        case 188: return KeyboardKey.Comma;
        case 190: return KeyboardKey.Period;
        case 191: return KeyboardKey.Slash;
        case 219: return KeyboardKey.BracketLeft;
        case 221: return KeyboardKey.BracketRight;
        case 220: return KeyboardKey.Backslash;
        case 186: return KeyboardKey.Semicolon;
        }

        return KeyboardKey.Unknown;
    }
}