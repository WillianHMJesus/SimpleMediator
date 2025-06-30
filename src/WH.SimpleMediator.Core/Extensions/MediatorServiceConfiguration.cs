using System;
using System.Collections.Generic;
using System.Reflection;

namespace WH.SimpleMediator.Extensions.Microsoft.DependencyInjection;

public class MediatorServiceConfiguration
{
    public List<Assembly> AssembliesToRegister { get; } = new List<Assembly>();
    public List<Type> BehaviorsToRegister { get; } = new List<Type>();

    public MediatorServiceConfiguration AddPipelineBehavior(Type preRequestHandlerType)
    {
        BehaviorsToRegister.Add(preRequestHandlerType);

        return this;
    }

    public MediatorServiceConfiguration RegisterServicesFromAssemblies(params Assembly[] assemblies)
    {
        AssembliesToRegister.AddRange(assemblies);

        return this;
    }
}
