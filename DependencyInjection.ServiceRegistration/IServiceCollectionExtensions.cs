using DependencyInjection.ServiceRegistration.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.ServiceRegistration
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        ///    Adds classes decorated with <see cref="RegisterServiceAttribute" /> as a service to the specified <see cref="IServiceCollection" />.
        ///    If no assembly is specified in <paramref name="assemblies" /> the calling assembly is used.
        /// </summary>
        public static IServiceCollection RegisterServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetCallingAssembly() };
            }

            var definedTypes = assemblies.SelectMany(x => x.DefinedTypes);
            foreach (var type in definedTypes)
            {
                if (!type.IsAbstract && !type.IsDefined(typeof(DoNotRegisterServiceAttribute), true) && type.GetCustomAttribute(typeof(RegisterServiceAttribute), true) is RegisterServiceAttribute registerServiceAttribute && registerServiceAttribute != null)
                {
                    services.Add(registerServiceAttribute.ServiceType, type, registerServiceAttribute.ServiceLifetime);
                }
            }

            return services;
        }

        /// <summary>
        ///    Registers multiple service instances of the same service type specified in <paramref name="serviceType" />;
        ///    all classes that inherit or implement the service type are added to <see cref="IServiceCollection" /> as an implementation type for the specified service type.
        ///    The addition is not made for a class decorated with <see cref="DoNotRegisterServiceAttribute" />.
        /// </summary>
        public static IServiceCollection AddMultiple(this IServiceCollection services, Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            foreach (var type in Assembly.GetAssembly(serviceType).DefinedTypes)
            {
                if (!type.IsClass || type.IsGenericType || type.IsAbstract || type.IsDefined(typeof(DoNotRegisterServiceAttribute), true))
                {
                    continue;
                }

                if (serviceType.IsAssignableFrom(type))
                {
                    services.Add(serviceType, type, lifetime);
                    continue;
                }

                if (serviceType.IsGenericType)
                {
                    if (!serviceType.IsInterface && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == serviceType)
                    {
                        var genericServiceType = serviceType.MakeGenericType(type.BaseType.GetGenericArguments());
                        services.Add(genericServiceType, type, lifetime);
                        continue;
                    }

                    if (type.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == serviceType) != null)
                    {
                        if (type.BaseType != typeof(object) && type.BaseType.IsGenericType)
                        {
                            var genericServiceType = serviceType.MakeGenericType(type.BaseType.GetGenericArguments());
                            services.Add(genericServiceType, type, lifetime);
                        }
                        else
                        {
                            var genericServiceType = type.GetInterfaces().FirstOrDefault(x => (x.IsGenericType ? x.GetGenericTypeDefinition() : x) == serviceType);
                            services.Add(genericServiceType, type, lifetime);
                        }
                    }
                }
            }

            return services;
        }

        private static void Add(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime serviceLifetime)
            => services.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));
    }
}
