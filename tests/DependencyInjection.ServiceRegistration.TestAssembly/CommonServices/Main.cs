using DependencyInjection.ServiceRegistration.Attributes;
using System;

namespace DependencyInjection.ServiceRegistration.TestAssembly.Classes
{
    public interface IService
    {
        Type Run();
    }

    public abstract class AbstractBase : IService
    {
        public abstract Type Run();
    }

    public class ServiceA : AbstractBase
    {
        public override Type Run() => GetType();
    }

    public class ServiceB : AbstractBase
    {
        public override Type Run() => GetType();
    }

    public class ServiceC : IService
    {
        public Type Run() => GetType();
    }

    [DoNotRegisterService]
    public class ServiceD : IService
    {
        public Type Run() => GetType();
    }
}
