namespace DependencyInjection.ServiceRegistration.TestAssembly.Classes
{
    public interface IService { }

    public abstract class AbstractBase : IService { }

    public class ServiceA : AbstractBase { }

    public class ServiceB : AbstractBase { }

    public class ServiceC : IService { }
}
