using DependencyInjection.ServiceRegistration.Attributes;
using System;
using System.Collections.Generic;

namespace DependencyInjection.ServiceRegistration.TestAssembly.GenericClasses
{
    public interface IGenericService<T>
    {
        Type Run();
    }

    public class GenericServiceA : IGenericService<A>
    {
        public Type Run() => GetType();
    }

    public class GenericServiceB : IGenericService<B>
    {
        public Type Run() => GetType();
    }

    public abstract class AbstractGenericBase<T1, T2>
    {
        public abstract Type Run();
    }

    public class GenericServiceAB : AbstractGenericBase<A, B>
    {
        public override Type Run() => GetType();
    }

    [DoNotRegisterService]
    public class GenericServiceBA : AbstractGenericBase<B, A>
    {
        public override Type Run() => GetType();
    }

    public class GenericServiceCEnumerableInt : AbstractGenericBase<C, IEnumerable<int>>
    {
        public override Type Run() => GetType();
    }

    public class B { }

    public class A { }

    public class C { }
}
