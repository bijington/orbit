using DrawingGame.GameObjects;
using Orbit.Engine;

namespace DrawingGame.Scenes;

public class MainScene : GameScene
{
	public MainScene(
		CountdownTimer countdownTimer,
		DrawingSurface drawingSurface)
	{
		Add(drawingSurface);
        Add(countdownTimer);
    }
}
