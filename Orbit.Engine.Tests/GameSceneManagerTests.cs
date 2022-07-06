using NUnit.Framework;
using Orbit.Engine.Tests.Mocks;

namespace Orbit.Engine.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        GameSceneManager manager = new(new MockDispatcher());

        //manager.State
    }
}