using FluentAssertions;
using NUnit.Framework;
using Orbit.Engine.Tests.Mocks;

namespace Orbit.Engine.Tests;

public class Tests
{
    [Test]
    public void InitialStateShouldBeEmpty()
    {
        GameSceneManager manager = new(new MockDispatcher(), null);

        manager.State.Should().Be(GameState.Empty);
    }

    [Test]
    public void LoadSceneShouldChangeStateToLoaded()
    {
        GameSceneManager manager = new(
            new MockDispatcher(),
            MockServiceScopeFactory.ThatProvides(
                () => MockServiceScope.WithServiceProvider(
                    MockServiceProvider.ThatProvides(
                        (typeof(MockGameScene), () => new MockGameScene())))));

        manager.LoadScene<MockGameScene>(new GameSceneView());

        manager.State.Should().Be(GameState.Loaded);
    }
}