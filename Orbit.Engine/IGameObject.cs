namespace Orbit.Engine;

public interface IGameObject : IDrawable
{
    RectF Bounds { get; }
}
