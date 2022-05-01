using Microsoft.Maui.Dispatching;

namespace Orbit.Engine;

public class GameSceneManager : IGameSceneManager
{
    private readonly IDispatcher dispatcher;
    private int callbackMilliseconds = 16;
	private GameState gameState;
	private GameSceneView gameSceneView;

	public GameState State => gameState;

	public GameSceneManager(IDispatcher dispatcher)
	{
        this.dispatcher = dispatcher;
    }

	public IGameScene CurrentScene { get; private set; }

	public void LoadScene(IGameScene gameScene, GameSceneView graphicsView)
    {
		CurrentScene = gameScene;

		gameSceneView = graphicsView;
		gameSceneView.Scene = gameScene;
		gameSceneView.Drawable = gameScene;
    }

	public GameObject FindCollision(GameObject gameObject)
    {
		return CurrentScene.FindCollision(gameObject);
    }

	public void Pause()
    {
		gameState = GameState.Paused;
	}

	public void Start()
	{
		gameState = GameState.Started;
		UpdateScene();
	}

	public void Stop()
	{
		gameState = GameState.Stopped;
	}

    private void UpdateScene()
	{
		if (gameState != GameState.Started)
        {
			return;
        }

		gameSceneView.Invalidate();

		dispatcher.DispatchDelayed(
			TimeSpan.FromMilliseconds(callbackMilliseconds),
			() =>
			{
				UpdateScene();
			});
	}
}
