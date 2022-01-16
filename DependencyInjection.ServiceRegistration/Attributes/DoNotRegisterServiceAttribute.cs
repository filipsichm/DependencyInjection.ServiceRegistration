using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.ServiceRegistration.Attributes
{
    /// <summary>
    ///  Specifies that a class to which the attribute is applied should be excluded from automatic service registration;
    ///  <see cref="IServiceCollectionExtensions.RegisterServices(IServiceCollection, System.Reflection.Assembly[])"/>,
    ///  <see cref="IServiceCollectionExtensions.AddMultiple(IServiceCollection, Type, ServiceLifetime)"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DoNotRegisterServiceAttribute : Attribute
    {
    }
}
