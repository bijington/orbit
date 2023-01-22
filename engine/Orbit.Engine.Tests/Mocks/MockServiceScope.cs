using System;
using Microsoft.Extensions.DependencyInjection;

namespace Orbit.Engine.Tests.Mocks;

public class MockServiceScope : IServiceScope
{
    private MockServiceScope(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public static IServiceScope WithServiceProvider(IServiceProvider serviceProvider) =>
        new MockServiceScope(serviceProvider);

    public IServiceProvider ServiceProvider { get; }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
