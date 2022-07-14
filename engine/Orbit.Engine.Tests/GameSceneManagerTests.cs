using FluentAssertions;
using NUnit.Framework;
using Orbit.Engine.Tests.Mocks;

namespace Orbit.Engine.Tests;

public class Tests
{
    [Test]
    public void InitialStateShouldBeEmpty()
    {
        GameSceneManager manager = new(new MockDispatcher());

        manager.State.Should().Be(GameState.Empty);
    }

    [Test]
    public void LoadSceneShouldChangeStateToLoaded()
    {
        GameSceneManager manager = new(new MockDispatcher());

        manager.LoadScene(new MockGameScene(), new GameSceneView());

        manager.State.Should().Be(GameState.Loaded);
    }
}