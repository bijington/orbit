namespace Orbit.Engine;

public interface IGameSceneManager
{
    GameState State { get; }

    event EventHandler<GameStateChangedEventArgs> StateChanged;

    GameObject FindCollision(GameObject gameObject);
    void GameOver();
    void Pause();
    void LoadScene(IGameScene gameScene, GameSceneView graphicsView);
    void Start();
    void Stop();
}
