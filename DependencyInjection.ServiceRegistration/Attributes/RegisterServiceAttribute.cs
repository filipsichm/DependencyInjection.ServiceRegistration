using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection.ServiceRegistration.Attributes
{
    /// <summary>
    ///  Specifies that a class to which the attribute is applied should be added as an implementation for a service
    ///  of the type specified in <see cref="ServiceType" /> to <see cref="IServiceCollection" />
    ///  by calling the extension method <see cref="IServiceCollectionExtensions.RegisterServices(IServiceCollection, System.Reflection.Assembly[])"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterServiceAttribute : Attribute
    {
        public Type ServiceType { get; }
        public ServiceLifetime ServiceLifetime { get; }

        public RegisterServiceAttribute(Type serviceType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            ServiceType = serviceType;
            ServiceLifetime = serviceLifetime;
        }
    }
}
