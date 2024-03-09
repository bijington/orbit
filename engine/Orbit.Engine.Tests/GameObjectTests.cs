namespace Orbit.Engine.Tests

public class GameObjectTests
{
    [Test]
    public void AddShouldCallOnAddedToNewChild()
    {
        var parent = new MockGameObject();
        var child = new MockGameObject();

        child.OnAddedCount.Should().Be(0);

        parent.Add(child);

        child.OnAddedCount.Should().Be(1);
    }

    [Test]
    public void RemoveShouldCallOnRemovedToNewChild()
    {
        var parent = new MockGameObject();
        var child = new MockGameObject();

        child.OnRemovedCount.Should().Be(0);

        parent.Add(child);

        child.OnRemovedCount.Should().Be(0);

        parent.Remove(child);

        child.OnRemovedCount.Should().Be(1);
    }

    [Test]
    public void RenderShouldCallRenderOnChildren()
    {
        var parent = new MockGameObject();
        var child = new MockGameObject();

        parent.Render(null, RectF.Zero);

        parent.RenderCount.Should().Be(1);
        child.RenderCount.Should().Be(0);

        parent.Add(child);

        parent.Render(null, RectF.Zero);

        parent.RenderCount.Should().Be(2);
        child.RenderCount.Should().Be(1);
    }

    [Test]
    public void UpdateShouldCallUpdateOnChildren()
    {
        var parent = new MockGameObject();
        var child = new MockGameObject();

        parent.Update(16);

        parent.UpdateCount.Should().Be(1);
        child.UpdateCount.Should().Be(0);

        parent.Add(child);

        parent.Update(16);

        parent.UpdateCount.Should().Be(2);
        child.UpdateCount.Should().Be(1);
    }
}