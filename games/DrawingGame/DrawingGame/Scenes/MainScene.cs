using DrawingGame.GameObjects;
using Orbit.Engine;

namespace DrawingGame.Scenes;

public class MainScene : GameScene
{
	public MainScene(
		ColorPalette colorPalette,
		DrawingSurface drawingSurface)
	{
		//Add(colorPalette);
		Add(drawingSurface);
	}
}
