using Android.Views;

namespace Orbit.Input;

// All the code in this file is only included on Android.
public partial class GameController
{
    public GameController(int deviceId)
    {
        Dpad = new Stick(this, nameof(Dpad));
        LeftStick = new Stick(this, nameof(LeftStick));
        RightStick = new Stick(this, nameof(RightStick));

        LeftShoulder = new Shoulder(this, nameof(LeftShoulder));
        RightShoulder = new Shoulder(this, nameof(RightShoulder));
    }
    
    public void OnGenericMotionEvent(MotionEvent motionEvent)
    {
        // Check that the event came from a game controller
        if (motionEvent.Source.HasFlag(InputSourceType.Joystick) &&
            motionEvent.Action == MotionEventActions.Move)
        {
            // Process all historical movement samples in the batch
            var historySize = motionEvent.HistorySize;
    
            // Process the movements starting from the
            // earliest historical position in the batch
            for (var i = 0; i < historySize; i++) 
            {
                // Process the event at historical position i
                ProcessJoystickInput(motionEvent, i);
            }
    
            // Process the current movement sample in the batch (position -1)
            ProcessJoystickInput(motionEvent, -1);
        }
    }
    
    private static float GetCenteredAxis(MotionEvent motionEvent, InputDevice device, Axis axis, int historyPos)
    {
        var range = device.GetMotionRange(axis, motionEvent.Source);

        // A joystick at rest does not always report an absolute position of
        // (0,0). Use the getFlat() method to determine the range of values
        // bounding the joystick axis center.
        if (range is not null) 
        {
            var flat = range.Flat;
            var value = historyPos < 0 ? motionEvent.GetAxisValue(axis):
                    motionEvent.GetHistoricalAxisValue(axis, historyPos);

            // Ignore axis values that are within the 'flat' region of the
            // joystick axis center.
            if (Math.Abs(value) > flat)
            {
                return value;
            }
        }
        return 0;
    }
    
    private void ProcessJoystickInput(MotionEvent motionEvent, int historyPos)
    {
        var inputDevice = motionEvent.Device;

        if (inputDevice is null)
        {
            return;
        }

        LeftStick.XAxis = GetCenteredAxis(motionEvent, inputDevice, Axis.X, historyPos);
        LeftStick.YAxis = GetCenteredAxis(motionEvent, inputDevice, Axis.Y, historyPos);

        RightStick.XAxis = GetCenteredAxis(motionEvent, inputDevice, Axis.Z, historyPos);
        RightStick.YAxis = GetCenteredAxis(motionEvent, inputDevice, Axis.Rz, historyPos);
        
        LeftShoulder.Trigger = GetCenteredAxis(motionEvent, inputDevice, Axis.Ltrigger, historyPos);
        RightShoulder.Trigger = GetCenteredAxis(motionEvent, inputDevice, Axis.Rtrigger, historyPos);
    }

    public void OnKeyDown(InputEvent inputEvent)
    {
        if (!IsDpadDevice(inputEvent))
        {
            return;
        }

        switch (inputEvent)
        {
            // If the input event is a MotionEvent, check its hat axis values.
            case MotionEvent motionEvent:
                // Use the hat axis value to find the D-pad direction
                Dpad.XAxis = motionEvent.GetAxisValue(Axis.HatX);
                Dpad.YAxis = motionEvent.GetAxisValue(Axis.HatY);
                break;
            
            // If the input event is a KeyEvent, check its key code.
            // Use the key code to find the D-pad direction.
            case KeyEvent { KeyCode: Keycode.DpadLeft }:
                Dpad.XAxis = -1;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadRight }:
                Dpad.XAxis = 1;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadDown }:
                Dpad.YAxis = -1;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadUp }:
                Dpad.YAxis = 1;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonL1 }:
                LeftShoulder.Button = true;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonR1 }:
                RightShoulder.Button = true;
                break;
        }
    }
    
    public void OnKeyUp(InputEvent inputEvent)
    {
        if (!IsDpadDevice(inputEvent))
        {
            return;
        }

        switch (inputEvent)
        {
            // If the input event is a MotionEvent, check its hat axis values.
            case MotionEvent:
                // Use the hat axis value to find the D-pad direction
                Dpad.XAxis = 0;
                Dpad.YAxis = 0;
                break;
            
            // If the input event is a KeyEvent, check its key code.
            // Use the key code to find the D-pad direction.
            case KeyEvent { KeyCode: Keycode.DpadLeft }:
                Dpad.XAxis = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadRight }:
                Dpad.XAxis = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadDown }:
                Dpad.YAxis = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadUp }:
                Dpad.YAxis = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonL1 }:
                LeftShoulder.Button = false;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonR1 }:
                RightShoulder.Button = false;
                break;
        }
    }
    
    private static bool IsDpadDevice(InputEvent inputEvent)
    {
        // Check that input comes from a device with directional pads.
        return inputEvent.Source.HasFlag(InputSourceType.Dpad);
    }
}
