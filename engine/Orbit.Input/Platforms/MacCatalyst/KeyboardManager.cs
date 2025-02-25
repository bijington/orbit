using Foundation;

using UIKit;

namespace Orbit.Input;

public partial class KeyboardManager
{
    public void PressesBegan(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        foreach (UIPress press in presses)
        {
            if (press.Key is null) continue;
            
            var mapped = ToKeyboardKey(press.Key.KeyCode);
            
            KeyboardKeyPressed(mapped);
        }
    }
    
    public void PressesCancelled(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        ReleaseKeys(presses);
    }

    void ReleaseKeys(NSSet<UIPress> presses)
    {
        foreach (UIPress press in presses)
        {
            if (press.Key is null) continue;
            
            var mapped = ToKeyboardKey(press.Key.KeyCode);
            
            KeyboardKeyReleased(mapped);
        }
    }

    public void PressesEnded(NSSet<UIPress> presses, UIPressesEvent evt)
    {
        ReleaseKeys(presses);
    }

    static KeyboardKey ToKeyboardKey(UIKeyboardHidUsage platformKey) => platformKey switch
    {
        UIKeyboardHidUsage.KeyboardA => KeyboardKey.KeyA,
        UIKeyboardHidUsage.KeyboardB => KeyboardKey.KeyB,
        UIKeyboardHidUsage.KeyboardC => KeyboardKey.KeyC,
        UIKeyboardHidUsage.KeyboardD => KeyboardKey.KeyD,
        UIKeyboardHidUsage.KeyboardE => KeyboardKey.KeyE,
        UIKeyboardHidUsage.KeyboardF => KeyboardKey.KeyF,
        UIKeyboardHidUsage.KeyboardG => KeyboardKey.KeyG,
        UIKeyboardHidUsage.KeyboardH => KeyboardKey.KeyH,
        UIKeyboardHidUsage.KeyboardJ => KeyboardKey.KeyJ,
        UIKeyboardHidUsage.KeyboardK => KeyboardKey.KeyK,
        UIKeyboardHidUsage.KeyboardL => KeyboardKey.KeyL,
        UIKeyboardHidUsage.KeyboardM => KeyboardKey.KeyM,
        UIKeyboardHidUsage.KeyboardI => KeyboardKey.KeyI,
        UIKeyboardHidUsage.KeyboardN => KeyboardKey.KeyN,
        UIKeyboardHidUsage.KeyboardO => KeyboardKey.KeyO,
        UIKeyboardHidUsage.KeyboardP => KeyboardKey.KeyP,
        UIKeyboardHidUsage.KeyboardQ => KeyboardKey.KeyQ,
        UIKeyboardHidUsage.KeyboardR => KeyboardKey.KeyR,
        UIKeyboardHidUsage.KeyboardS => KeyboardKey.KeyS,
        UIKeyboardHidUsage.KeyboardT => KeyboardKey.KeyT,
        UIKeyboardHidUsage.KeyboardU => KeyboardKey.KeyU,
        UIKeyboardHidUsage.KeyboardV => KeyboardKey.KeyV,
        UIKeyboardHidUsage.KeyboardW => KeyboardKey.KeyW,
        UIKeyboardHidUsage.KeyboardX => KeyboardKey.KeyX,
        UIKeyboardHidUsage.KeyboardY => KeyboardKey.KeyY,
        UIKeyboardHidUsage.KeyboardZ => KeyboardKey.KeyZ,
        UIKeyboardHidUsage.Keyboard1 => KeyboardKey.Digit1,
        UIKeyboardHidUsage.Keyboard2 => KeyboardKey.Digit2,
        UIKeyboardHidUsage.Keyboard3 => KeyboardKey.Digit3,
        UIKeyboardHidUsage.Keyboard4 => KeyboardKey.Digit4,
        UIKeyboardHidUsage.Keyboard5 => KeyboardKey.Digit5,
        UIKeyboardHidUsage.Keyboard6 => KeyboardKey.Digit6,
        UIKeyboardHidUsage.Keyboard7 => KeyboardKey.Digit7,
        UIKeyboardHidUsage.Keyboard8 => KeyboardKey.Digit8,
        UIKeyboardHidUsage.Keyboard9 => KeyboardKey.Digit9,
        UIKeyboardHidUsage.Keyboard0 => KeyboardKey.Digit0,
        UIKeyboardHidUsage.KeyboardReturnOrEnter => KeyboardKey.Enter,
        UIKeyboardHidUsage.KeyboardEscape => KeyboardKey.Escape,
        UIKeyboardHidUsage.KeyboardDeleteOrBackspace => KeyboardKey.Backspace,
        UIKeyboardHidUsage.KeyboardTab => KeyboardKey.Tab,
        UIKeyboardHidUsage.KeyboardSpacebar => KeyboardKey.Space,
        UIKeyboardHidUsage.KeyboardHyphen => KeyboardKey.Minus,
        UIKeyboardHidUsage.KeyboardEqualSign => KeyboardKey.Equal,
        UIKeyboardHidUsage.KeyboardOpenBracket => KeyboardKey.BracketLeft,
        UIKeyboardHidUsage.KeyboardCloseBracket => KeyboardKey.BracketRight,
        UIKeyboardHidUsage.KeyboardBackslash => KeyboardKey.Backslash,
        UIKeyboardHidUsage.KeyboardSemicolon => KeyboardKey.Semicolon,
        UIKeyboardHidUsage.KeyboardQuote => KeyboardKey.Quote,
        UIKeyboardHidUsage.KeyboardComma => KeyboardKey.Comma,
        UIKeyboardHidUsage.KeyboardPeriod => KeyboardKey.Period,
        UIKeyboardHidUsage.KeyboardSlash => KeyboardKey.Slash,
        UIKeyboardHidUsage.KeyboardCapsLock => KeyboardKey.CapsLock,
        UIKeyboardHidUsage.KeyboardF1 => KeyboardKey.F1,
        UIKeyboardHidUsage.KeyboardF2 => KeyboardKey.F2,
        UIKeyboardHidUsage.KeyboardF3 => KeyboardKey.F3,
        UIKeyboardHidUsage.KeyboardF4 => KeyboardKey.F4,
        UIKeyboardHidUsage.KeyboardF5 => KeyboardKey.F5,
        UIKeyboardHidUsage.KeyboardF6 => KeyboardKey.F6,
        UIKeyboardHidUsage.KeyboardF7 => KeyboardKey.F7,
        UIKeyboardHidUsage.KeyboardF8 => KeyboardKey.F7,
        UIKeyboardHidUsage.KeyboardF9 => KeyboardKey.F9,
        UIKeyboardHidUsage.KeyboardF10 => KeyboardKey.F10,
        UIKeyboardHidUsage.KeyboardF11 => KeyboardKey.F11,
        UIKeyboardHidUsage.KeyboardF12 => KeyboardKey.F12,
        UIKeyboardHidUsage.KeyboardPrintScreen => KeyboardKey.PrintScreen,
        UIKeyboardHidUsage.KeyboardScrollLock => KeyboardKey.ScrollLock,
        UIKeyboardHidUsage.KeyboardPause => KeyboardKey.Pause,
        UIKeyboardHidUsage.KeyboardInsert => KeyboardKey.Insert,
        UIKeyboardHidUsage.KeyboardHome => KeyboardKey.Home,
        UIKeyboardHidUsage.KeyboardPageUp => KeyboardKey.PageUp,
        UIKeyboardHidUsage.KeyboardDeleteForward => KeyboardKey.Delete,
        UIKeyboardHidUsage.KeyboardEnd => KeyboardKey.End,
        UIKeyboardHidUsage.KeyboardPageDown => KeyboardKey.PageDown,
        UIKeyboardHidUsage.KeyboardRightArrow => KeyboardKey.ArrowRight,
        UIKeyboardHidUsage.KeyboardLeftArrow => KeyboardKey.ArrowLeft,
        UIKeyboardHidUsage.KeyboardDownArrow => KeyboardKey.ArrowDown,
        UIKeyboardHidUsage.KeyboardUpArrow => KeyboardKey.ArrowUp,
        UIKeyboardHidUsage.KeypadNumLock => KeyboardKey.NumLock,
        UIKeyboardHidUsage.KeypadSlash => KeyboardKey.IntBackslash,
        UIKeyboardHidUsage.KeypadAsterisk => KeyboardKey.NumpadMultiply,
        UIKeyboardHidUsage.KeypadHyphen => KeyboardKey.NumpadSubtract,
        UIKeyboardHidUsage.KeypadPlus => KeyboardKey.NumpadAdd,
        UIKeyboardHidUsage.KeypadEnter => KeyboardKey.Enter,
        UIKeyboardHidUsage.Keypad1 => KeyboardKey.Numpad1,
        UIKeyboardHidUsage.Keypad2 => KeyboardKey.Numpad2,
        UIKeyboardHidUsage.Keypad3 => KeyboardKey.Numpad3,
        UIKeyboardHidUsage.Keypad4 => KeyboardKey.Numpad4,
        UIKeyboardHidUsage.Keypad5 => KeyboardKey.Numpad5,
        UIKeyboardHidUsage.Keypad6 => KeyboardKey.Numpad6,
        UIKeyboardHidUsage.Keypad7 => KeyboardKey.Numpad7,
        UIKeyboardHidUsage.Keypad8 => KeyboardKey.Numpad8,
        UIKeyboardHidUsage.Keypad9 => KeyboardKey.Numpad9,
        UIKeyboardHidUsage.Keypad0 => KeyboardKey.Numpad0,
        UIKeyboardHidUsage.KeypadPeriod => KeyboardKey.NumpadDecimal,
        UIKeyboardHidUsage.KeyboardNonUSBackslash => KeyboardKey.IntBackslash,
        UIKeyboardHidUsage.KeyboardApplication => KeyboardKey.LaunchApplication1,
        UIKeyboardHidUsage.KeypadEqualSign => KeyboardKey.Equal,
        UIKeyboardHidUsage.KeyboardMenu => KeyboardKey.ContextMenu,
        UIKeyboardHidUsage.KeyboardLeftControl => KeyboardKey.ControlLeft,
        UIKeyboardHidUsage.KeyboardLeftShift => KeyboardKey.ShiftLeft,
        UIKeyboardHidUsage.KeyboardLeftAlt => KeyboardKey.AltLeft,
        UIKeyboardHidUsage.KeyboardRightControl => KeyboardKey.ControlRight,
        UIKeyboardHidUsage.KeyboardRightShift => KeyboardKey.ShiftRight,
        UIKeyboardHidUsage.KeyboardRightAlt => KeyboardKey.AltRight,
        _ => KeyboardKey.Unknown
    };
}
