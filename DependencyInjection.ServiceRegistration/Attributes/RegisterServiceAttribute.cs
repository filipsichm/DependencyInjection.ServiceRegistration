using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection.ServiceRegistration.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
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
