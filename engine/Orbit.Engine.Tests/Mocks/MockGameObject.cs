using Microsoft.Maui.Graphics;

using Orbit.Engine;

namespace Orbit.Engine.Tests;

public class MockGameObject : GameObject
{
    internal int OnAddedCount { get; private set; }

    internal int OnRemovedCount { get; private set; }

    internal int RenderCount { get; private set; }

    internal int UpdateCount { get; private set; }

    public override void OnAdded()
    {
    }

    public override void OnRemoved()
    {
    }

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