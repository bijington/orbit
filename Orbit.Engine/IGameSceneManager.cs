namespace Orbit.Engine;

public interface IGameSceneManager
{
    GameState State { get; }

    GameObject FindCollision(GameObject gameObject);
    void Pause();
    void LoadScene(IGameScene gameScene, GameSceneView graphicsView);
    void Start();
    void Stop();
}
