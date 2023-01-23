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
        var sceneInstance = new MockGameScene();
        GameSceneManager manager = new(
            new MockDispatcher(),
            MockServiceScopeFactory.ThatProvides(
                () => MockServiceScope.WithServiceProvider(
                    MockServiceProvider.ThatProvides(
                        (typeof(MockGameScene), () => sceneInstance)))));

        manager.LoadScene<MockGameScene>(new GameSceneView());

        manager.State.Should().Be(GameState.Loaded);

        manager.CurrentScene.Should().Be(sceneInstance);
    }
}