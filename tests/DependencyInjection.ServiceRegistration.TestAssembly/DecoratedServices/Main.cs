using DependencyInjection.ServiceRegistration.Attributes;
using System;

namespace DependencyInjection.ServiceRegistration.TestAssembly.AttributeDecoratedClasses
{
    public interface IDecoratedService
    {
        Type Run();
    }

    [RegisterService(typeof(IDecoratedService))]
    public abstract class AbstractDecoratedBase : IDecoratedService
    {
        public abstract Type Run();
    }

    public class DecoratedClassA : AbstractDecoratedBase
    {
        public override Type Run() => GetType();
    }

    public class DecoratedClassB : AbstractDecoratedBase
    {
        public override Type Run() => GetType();
    }

    [DoNotRegisterService]
    public class DecoratedClassC : AbstractDecoratedBase
    {
        public override Type Run() => GetType();
    }

    [RegisterService(typeof(IDecoratedService))]
    public class DirectDecoratedClass : IDecoratedService
    {
        public Type Run() => GetType();
    }
}
