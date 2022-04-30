namespace Orbit.Engine;

public interface IGameSceneManager
{
    GameState State { get; }

    void Pause();
    void RegisterScene(IGameScene gameScene, GameSceneView graphicsView);
    void Start();
    void Stop();
}
