using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SingletonDI;

public static class AttributedSingletonServiceCollectionExtensions
{
    public static IServiceCollection AddAttributedSingletons(
        this IServiceCollection services,
        Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(assembly);

        var implementations = assembly
            .DefinedTypes
            .Where(type =>
                type.IsClass
                && !type.IsAbstract
                && type.GetCustomAttribute<SingletonAttribute>() is not null)
            .ToArray();

        foreach (var implementation in implementations)
        {
            var implementationType = implementation.AsType();
            var serviceTypes = implementation.GetInterfaces();

            if (serviceTypes.Length == 0)
            {
                services.AddSingleton(implementationType);
                continue;
            }

            services.AddSingleton(implementationType);
            foreach (var serviceType in serviceTypes)
            {
                services.AddSingleton(
                    serviceType,
                    provider => provider.GetRequiredService(implementationType));
            }
        }

        return services;
    }
}
