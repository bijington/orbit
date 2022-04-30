using Microsoft.Maui.Dispatching;

namespace Orbit.Engine;

public class GameSceneManager : IGameSceneManager
{
    private readonly IDispatcher dispatcher;
    private int callbackMilliseconds = 16;
	private GameState gameState;
	private GameSceneView gameSceneView;

	//private readonly IList<GameObject> gameObjects;

	public GameState State => gameState;

	public GameSceneManager(IDispatcher dispatcher)
	{
        this.dispatcher = dispatcher;
    }

	// TODO: Make this bindable so it can be set in the UI?
	public IGameScene CurrentScene { get; }

	public void RegisterScene(IGameScene gameScene, GameSceneView graphicsView)
    {
		gameSceneView = graphicsView;
		gameSceneView.Scene = gameScene;
		gameSceneView.Drawable = gameScene;

		// Weak event handling?
		gameSceneView.EndInteraction += GraphicsView_EndInteraction;
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

	private void GraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        
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
