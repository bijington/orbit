namespace Orbit.Engine;

public interface IGameScene : IDrawable
{
    GameObject FindCollision(GameObject gameObject);
}
