using Microsoft.Maui.Dispatching;
using PerfLab.FpsStats;
using Orbit.Engine;
using Orbit.Scenes;

namespace Orbit;

public partial class MainPage : ContentPage
{
    private readonly IGameSceneManager gameSceneManager;
    private readonly MainScene mainScene;
    private readonly FpsStatsService fpsService;

	public static bool ShowBounds { get; } = true;

	public static TouchMode TouchMode { get; private set; }

	public MainPage(
		IGameSceneManager gameSceneManager,
		HomeScene scene,
		MainScene mainScene)
	{
		InitializeComponent();

		fpsService = new FpsStatsService();
		fpsService.Start(action => Dispatcher.DispatchAsync(action));
		fpsService.StatsUpdated += FpsService_StatsUpdated;
        this.gameSceneManager = gameSceneManager;
        this.mainScene = mainScene;
        gameSceneManager.LoadScene(scene, GameView);

		gameSceneManager.Start();
		//gameSceneManager.Pause();
	}

	// TODO: GameObject
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

	void PlayButton_Clicked(System.Object sender, System.EventArgs e)
	{
		gameSceneManager.LoadScene(mainScene, GameView);
	}
}
