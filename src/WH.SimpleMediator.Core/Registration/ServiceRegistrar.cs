using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WH.SimpleMediator
{
    public static class ServiceRegistrar
    {
        public static void RegisterHandlers(IServiceCollection services, List<Assembly> assemblies, Type handlerInterface)
        {
            var types = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == handlerInterface);

                foreach (var iface in interfaces)
                {
                    services.AddTransient(iface, type);
                }
            }
        }

        public static void RegisterPipelineBehaviors(IServiceCollection services, List<Type> types)
        {
            foreach (var type in types)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), type);
            }
        }
    }
}
