using Microsoft.Maui.Dispatching;
using PerfLab.FpsStats;
using Orbit.Engine;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly Scene scene;
	private readonly FpsStatsService fpsService;

	public static bool ShowBounds { get; } = true;

	public static TouchMode TouchMode { get; private set; }

	public MainPage(IGameSceneManager gameSceneManager, Scene scene)
	{
		InitializeComponent();

		fpsService = new FpsStatsService();
		fpsService.Start(action => Dispatcher.DispatchAsync(action));
		fpsService.StatsUpdated += FpsService_StatsUpdated;
        this.gameSceneManager = gameSceneManager;
        this.scene = scene;

		gameSceneManager.RegisterScene(scene, GameView);

		gameSceneManager.Start();
		//Update();
	}

	private void Update()
	{
		GameView.Invalidate();

		this.Dispatcher.DispatchDelayed(
			TimeSpan.FromMilliseconds(16),
			() =>
			{
				Update();
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

	private void FpsService_StatsUpdated(object sender, EventArgs e)
	{
		Texty.Text = fpsService.Stats;
	}

	public void SetText(string text)
    {
		Texty.Text = text;
    }

	bool isPaused;

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
		if (gameSceneManager.State == GameState.Paused)
		{
			this.gameSceneManager.Start();
		}
		else
        {
			this.gameSceneManager.Pause();
        }
    }
}
