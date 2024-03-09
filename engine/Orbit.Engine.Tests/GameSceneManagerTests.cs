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

    [Test]
    public void CompleteShouldMoveStateToCompleted()
    {
        GameSceneManager manager = new(new MockDispatcher(), null);

        manager.Complete();

        manager.State.Should().Be(GameState.Completed);
    }

    [Test]
    public void GameOverShouldMoveStateToGameOver()
    {
        GameSceneManager manager = new(new MockDispatcher(), null);

        manager.GameOver();

        manager.State.Should().Be(GameState.GameOver);
    }

    [Test]
    public void PauseShouldMoveStateToPaused()
    {
        GameSceneManager manager = new(new MockDispatcher(), null);

        manager.Pause();

        manager.State.Should().Be(GameState.Paused);
    }

    [Test]
    public void StartShouldMoveStateToStarted()
    {
        GameSceneManager manager = new(new MockDispatcher(), null);

        manager.Start();

        manager.State.Should().Be(GameState.Started);
    }

    [Test]
    public void StopShouldMoveStateToLoaded()
    {
        GameSceneManager manager = new(new MockDispatcher(), null);

        manager.Stop();

        manager.State.Should().Be(GameState.Loaded);
    }
}