using DependencyInjection.ServiceRegistration.Attributes;

namespace DependencyInjection.ServiceRegistration.TestAssembly.AttributeDecoratedClasses
{
    public interface IDecoratedService { }

    [RegisterService(typeof(IDecoratedService))]
    public abstract class AbstractDecoratedBase : IDecoratedService { }

    public class DecoratedClassA : AbstractDecoratedBase { }

    public class DecoratedClassB : AbstractDecoratedBase { }

    [DoNotRegisterService]
    public class DecoratedClassC : AbstractDecoratedBase { }

    [RegisterService(typeof(IDecoratedService))]
    public class DirectDecoratedClass { }
}
