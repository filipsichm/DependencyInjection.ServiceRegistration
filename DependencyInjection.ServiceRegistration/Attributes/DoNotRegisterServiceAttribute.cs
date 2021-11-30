using System;

namespace DependencyInjection.ServiceRegistration.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DoNotRegisterServiceAttribute : Attribute
    {
    }
}
