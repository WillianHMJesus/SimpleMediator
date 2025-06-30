using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WH.SimpleMediator.Extensions.Microsoft.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimpleMediator(
        this IServiceCollection services,
        Action<MediatorServiceConfiguration> configuration)
    {
        var serviceConfig = new MediatorServiceConfiguration();
        configuration.Invoke(serviceConfig);

        return services.AddSimpleMediator(serviceConfig);
    }

    public static IServiceCollection AddSimpleMediator(
       this IServiceCollection services,
       MediatorServiceConfiguration configuration)
    {
        if (!configuration.AssembliesToRegister.Any())
        {
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
        }

        services.AddSingleton<IMediator, Mediator>();

        ServiceRegistrar.RegisterHandlers(services, configuration.AssembliesToRegister, typeof(INotificationHandler<>));
        ServiceRegistrar.RegisterHandlers(services, configuration.AssembliesToRegister, typeof(IRequestHandler<,>));
        ServiceRegistrar.RegisterPipelineBehaviors(services, configuration.BehaviorsToRegister);

        return services;
    }
}
