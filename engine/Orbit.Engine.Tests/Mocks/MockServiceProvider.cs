using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbit.Engine.Tests.Mocks;

public class MockServiceProvider : IServiceProvider
{
    private readonly IDictionary<Type, Func<object>> providers;

    private MockServiceProvider(IDictionary<Type, Func<object>> providers)
    {
        this.providers = providers;
    }

    public static IServiceProvider ThatProvides(params (Type, Func<object>)[] providers) =>
        new MockServiceProvider(providers.ToDictionary(v => v.Item1, v => v.Item2));

    public object? GetService(Type serviceType)
    {
        return this.providers[serviceType].Invoke();
    }
}
