using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LifeCounter.Common.Container;

public static class DependenciesAutoRegisterer
{
    public static void Register(IServiceCollection serviceCollection, Assembly assembly)
    {
        var types = assembly
            .GetTypes()
            .Where(e => !e.IsAbstract)
            .Where(e => !e.IsNested)
            .Where(e => !e.IsEnum)
            .Where(e => e.IsPublic)
            .Where(e => e.IsClass)
            .Where(e => !HasPreventAutoRegistration(e))
            .Where(e => e.GetProperties().Length == 0);

        foreach (var type in types)
        {
            serviceCollection.TryAddScoped(type);

            foreach (var @interface in type.GetInterfaces().Where(e => !HasPreventAutoRegistration(e)))
            {
                serviceCollection.TryAddScoped(@interface, type);
            }
        }
    }

    private static bool HasPreventAutoRegistration(Type type)
    {
        return type.GetCustomAttribute<PreventAutoRegistrationAttribute>() == null;
    }
}