using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SingletonDI;
using Xunit;

namespace SingletonDITest;

public class SingletonDITest
{
    [Fact]
    public void ResolvesTheSameInstanceWhenRegisteredAsSingleton()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILoanAuditLogger, BuiltInLoanAuditLogger>();
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<ILoanAuditLogger>();
        var second = provider.GetRequiredService<ILoanAuditLogger>();

        first.ShouldBeSameAs(second);
        first.ShouldBeOfType<BuiltInLoanAuditLogger>();
    }

    [Fact]
    public void ResolvesDifferentInstancesWhenRegisteredAsTransient()
    {
        var services = new ServiceCollection();
        services.AddTransient<ILoanAuditLogger, BuiltInLoanAuditLogger>();
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<ILoanAuditLogger>();
        var second = provider.GetRequiredService<ILoanAuditLogger>();

        first.ShouldNotBeSameAs(second);
    }

    [Fact]
    public void ResolvesTheSameScopedInstanceWithinAScopeAndDifferentInstancesAcrossScopes()
    {
        var services = new ServiceCollection();
        services.AddScoped<ILoanAuditLogger, BuiltInLoanAuditLogger>();
        using var provider = services.BuildServiceProvider();
        using var firstScope = provider.CreateScope();
        using var secondScope = provider.CreateScope();

        var first = firstScope.ServiceProvider.GetRequiredService<ILoanAuditLogger>();
        var second = firstScope.ServiceProvider.GetRequiredService<ILoanAuditLogger>();
        var third = secondScope.ServiceProvider.GetRequiredService<ILoanAuditLogger>();

        first.ShouldBeSameAs(second);
        first.ShouldNotBeSameAs(third);
    }

    [Fact]
    public void RegistersSingletonAttributedServicesAsSharedSingletonInstances()
    {
        var services = new ServiceCollection();
        services.AddAttributedSingletons(typeof(AttributedLoanAuditLogger).Assembly);
        using var provider = services.BuildServiceProvider();

        var firstByInterface = provider.GetRequiredService<ILoanAuditLogger>();
        var secondByInterface = provider.GetRequiredService<ILoanAuditLogger>();
        var byConcreteType = provider.GetRequiredService<AttributedLoanAuditLogger>();

        firstByInterface.ShouldBeSameAs(secondByInterface);
        firstByInterface.ShouldBeSameAs(byConcreteType);
    }

    [Fact]
    public void AttributeScannerDoesNotRegisterUnmarkedBuiltInLogger()
    {
        var services = new ServiceCollection();
        services.AddAttributedSingletons(typeof(AttributedLoanAuditLogger).Assembly);
        using var provider = services.BuildServiceProvider();

        Should.Throw<InvalidOperationException>(
            () => provider.GetRequiredService<BuiltInLoanAuditLogger>());
    }
}
