namespace Orbit;

public class UserInputManager
{
	public UserInputMode Mode { get; set; }

    public TouchMode TouchMode { get; private set; }

    public void FinishTouch()
    {
        if (Mode != UserInputMode.TouchOnScreen)
        {
            return;
        }

        TouchMode = TouchMode.None;
    }

	public void HandleTouch(float touchX, double gameViewWidth)
	{
        if (Mode != UserInputMode.TouchOnScreen)
        {
            return;
        }

        var middle = gameViewWidth / 2;

        if (touchX >= middle)
        {
            TouchMode = TouchMode.SpeedUp;
        }
        else
        {
            TouchMode = TouchMode.SlowDown;
        }
    }

    public void SetInputMode(UserInputMode userInputMode)
    {
        Mode = userInputMode;
    }

    public void SetTouchMode(TouchMode touchMode)
    {
        if (Mode != UserInputMode.Buttons)
        {
            return;
        }

        TouchMode = touchMode;
    }
}
