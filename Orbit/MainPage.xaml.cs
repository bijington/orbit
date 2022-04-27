namespace Orbit;

public partial class MainPage : ContentPage
{
	private readonly Scene scene;

	public static TouchMode TouchMode { get; private set; }

	public MainPage()
	{
		InitializeComponent();

		scene = new Scene();
		GameView.Drawable = scene;

		Move();
	}

	private void Move()
	{
		GameView.Invalidate();

		this.Dispatcher.DispatchDelayed(
			TimeSpan.FromMilliseconds(16),
			() =>
			{
				Move();
			});
	}

	void GameView_EndInteraction(object sender, TouchEventArgs e)
	{
		TouchMode = TouchMode.None;
	}

	void GameView_StartInteraction(object sender, TouchEventArgs e)
	{
		var middle = GameView.Width / 2;

		var touchX = e.Touches.First().X;

		if (touchX >= middle)
		{
			TouchMode = TouchMode.SpeedUp;
		}
		else
		{
			TouchMode = TouchMode.SlowDown;
		}
	}
}
