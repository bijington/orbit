namespace BuildingGames;

public partial class ControllerManager
{
	private ControllerButton currentPressedButton;

	public event Action<ControllerButton> ButtonPressed;

	public ControllerButton CurrentPressedButton
	{
		get => this.currentPressedButton;
		private set
		{
			if (this.currentPressedButton != value)
			{
				this.currentPressedButton = value;

				if (value != ControllerButton.None)
				{
					ButtonPressed?.Invoke(value);
				}
			}
        }
	}

	public ControlMode Mode { get; private set; }

	public PointF PointerLocation { get; set; }

	public float PointerRadius { get; set; } = 4f;

	public PointF DirectionalChange { get; set; }
}

public enum ControllerButton
{
	None,
	Start,
	Right,
	Left,
	Up,
	Down,
	Accept,
	NavigateForward,
	NavigateBackward
}

public enum ControlMode
{
	Navigation,
	Pointer
}
