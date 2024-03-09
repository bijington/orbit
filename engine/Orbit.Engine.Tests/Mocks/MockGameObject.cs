using Orbit.Engine;

namespace Orbit.Engine.Tests;

public class MockGameObject : GameObject
{
    internal int RenderCount { get; private set; }

    internal int UpdateCount { get; private set; }

    public override void Render(ICanvas canvas, RectF dimensions)
    {
        base.Render(canvas, dimensions);

        RenderCount++;
    }

    public override void Update(double millisecondsSinceLastUpdate)
    {
        base.Update(millisecondsSinceLastUpdate);

        UpdateCount++;
    }
}