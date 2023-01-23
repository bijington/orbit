using System;
using Microsoft.Extensions.DependencyInjection;

namespace Orbit.Engine.Tests.Mocks;

public class MockServiceScopeFactory : IServiceScopeFactory
{
    private readonly Func<IServiceScope> serviceScopeProvider;

    private MockServiceScopeFactory(Func<IServiceScope> serviceScopeProvider)
    {
        this.serviceScopeProvider = serviceScopeProvider;
    }

    public static IServiceScopeFactory ThatProvides(Func<IServiceScope> serviceScopeProvider) =>
        new MockServiceScopeFactory(serviceScopeProvider);

    public IServiceScope CreateScope() =>
        serviceScopeProvider.Invoke();
}
