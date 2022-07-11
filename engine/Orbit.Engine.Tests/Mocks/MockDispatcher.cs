using System;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;

namespace Orbit.Engine.Tests.Mocks;

public class MockDispatcher : IDispatcher
{
    public bool IsDispatchRequired => false;

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
        Task.Run(async () =>
        {
            await Task.Delay(delay);
            action.Invoke();
        });

        return true;
    }
}
