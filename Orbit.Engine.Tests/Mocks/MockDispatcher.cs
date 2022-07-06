using System;
using Microsoft.Maui.Dispatching;

namespace Orbit.Engine.Tests.Mocks;

public class MockDispatcher : IDispatcher
{
    public bool IsDispatchRequired => throw new NotImplementedException();

    public IDispatcherTimer CreateTimer()
    {
        throw new NotImplementedException();
    }

    public bool Dispatch(Action action)
    {
        throw new NotImplementedException();
    }

    public bool DispatchDelayed(TimeSpan delay, Action action)
    {
        throw new NotImplementedException();
    }
}
