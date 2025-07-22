using Android.Views;

namespace Orbit.Input;

// All the code in this file is only included on Android.
public partial class GameController
{
    private readonly int deviceId;

    public GameController(int deviceId, string name)
    {
        this.deviceId = deviceId;
        Name = name;
        
        Dpad = new Stick(this, nameof(Dpad));
        LeftStick = new Stick(this, nameof(LeftStick));
        RightStick = new Stick(this, nameof(RightStick));

        LeftShoulder = new Shoulder(this, nameof(LeftShoulder));
        RightShoulder = new Shoulder(this, nameof(RightShoulder));
        
        North = new ButtonValue<bool>(this, nameof(North));
        South = new ButtonValue<bool>(this, nameof(South));
        East = new ButtonValue<bool>(this, nameof(East));
        West = new ButtonValue<bool>(this, nameof(West));
        Pause = new ButtonValue<bool>(this, nameof(Pause));
    }
    
    public bool OnGenericMotionEvent(MotionEvent motionEvent)
    {
        if (motionEvent.DeviceId != this.deviceId)
        {
            return false;
        }
        
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
        
        return true;
    }

    public bool OnKeyDown(InputEvent inputEvent)
    {
        if (inputEvent.DeviceId != this.deviceId)
        {
            return false;
        }
        
        if (!IsDpadDevice(inputEvent))
        {
            return false;
        }

        switch (inputEvent)
        {
            // If the input event is a MotionEvent, check its hat axis values.
            case MotionEvent motionEvent:
                // Use the hat axis value to find the D-pad direction
                Dpad.XAxis.Value = motionEvent.GetAxisValue(Axis.HatX);
                Dpad.YAxis.Value = motionEvent.GetAxisValue(Axis.HatY);
                break;
            
            // If the input event is a KeyEvent, check its key code.
            // Use the key code to find the D-pad direction.
            case KeyEvent { KeyCode: Keycode.DpadLeft }:
                Dpad.XAxis.Value = -1;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadRight }:
                Dpad.XAxis.Value = 1;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadDown }:
                Dpad.YAxis.Value = -1;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadUp }:
                Dpad.YAxis.Value = 1;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonL1 }:
                LeftShoulder.Button.Value = true;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonR1 }:
                RightShoulder.Button.Value = true;
                break;
            
            case KeyEvent keyEvent:
                RaiseUnmappedButtonChange(keyEvent.KeyCode.ToString(), false);
                break;
        }

        return true;
    }
    
    public bool OnKeyUp(InputEvent inputEvent)
    {
        if (inputEvent.DeviceId != this.deviceId)
        {
            return false;
        }
        
        if (!IsDpadDevice(inputEvent))
        {
            return false;
        }

        switch (inputEvent)
        {
            // If the input event is a MotionEvent, check its hat axis values.
            case MotionEvent:
                // Use the hat axis value to find the D-pad direction
                Dpad.XAxis.Value = 0;
                Dpad.YAxis.Value = 0;
                break;
            
            // If the input event is a KeyEvent, check its key code.
            // Use the key code to find the D-pad direction.
            case KeyEvent { KeyCode: Keycode.DpadLeft }:
                Dpad.XAxis.Value = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadRight }:
                Dpad.XAxis.Value = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadDown }:
                Dpad.YAxis.Value = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.DpadUp }:
                Dpad.YAxis.Value = 0;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonL1 }:
                LeftShoulder.Button.Value = false;
                break;
            
            case KeyEvent { KeyCode: Keycode.ButtonR1 }:
                RightShoulder.Button.Value = false;
                break;
            
            case KeyEvent keyEvent:
                RaiseUnmappedButtonChange(keyEvent.KeyCode.ToString(), false);
                break;
        }

        return true;
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
    
    private static bool IsDpadDevice(InputEvent inputEvent)
    {
        // Check that input comes from a device with directional pads.
        return inputEvent.Source.HasFlag(InputSourceType.Dpad);
    }
    
    private void ProcessJoystickInput(MotionEvent motionEvent, int historyPos)
    {
        var inputDevice = motionEvent.Device;

        if (inputDevice is null)
        {
            return;
        }

        LeftStick.XAxis.Value = GetCenteredAxis(motionEvent, inputDevice, Axis.X, historyPos);
        LeftStick.YAxis.Value = GetCenteredAxis(motionEvent, inputDevice, Axis.Y, historyPos);

        RightStick.XAxis.Value = GetCenteredAxis(motionEvent, inputDevice, Axis.Z, historyPos);
        RightStick.YAxis.Value = GetCenteredAxis(motionEvent, inputDevice, Axis.Rz, historyPos);
        
        LeftShoulder.Trigger.Value = GetCenteredAxis(motionEvent, inputDevice, Axis.Ltrigger, historyPos);
        RightShoulder.Trigger.Value = GetCenteredAxis(motionEvent, inputDevice, Axis.Rtrigger, historyPos);
    }
}
