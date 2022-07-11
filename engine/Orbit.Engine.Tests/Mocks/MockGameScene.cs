using Microsoft.Maui.Graphics;

namespace Orbit.Engine.Tests.Mocks;

public class MockGameScene : IGameScene
{
    public void Add(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        throw new System.NotImplementedException();
    }

    public GameObject FindCollision(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public void Remove(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public void Render(ICanvas canvas, RectF dimensions)
    {
        throw new System.NotImplementedException();
    }

    public void Update(double millisecondsSinceLastUpdate)
    {
        throw new System.NotImplementedException();
    }
}
