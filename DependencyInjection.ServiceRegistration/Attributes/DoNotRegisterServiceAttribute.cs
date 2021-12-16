using System;

namespace DependencyInjection.ServiceRegistration.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DoNotRegisterServiceAttribute : Attribute
    {
    }
}
