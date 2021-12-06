using DependencyInjection.ServiceRegistration.Attributes;
using System;
using System.Collections.Generic;

namespace DependencyInjection.ServiceRegistration.TestAssembly.GenericClasses
{
    public interface IGenericService<T> { }

    public class GenericServiceA : IGenericService<A> { }

    public class GenericServiceB : IGenericService<B> { }

    public interface IGenericService<T1, T2>
    {
        T2 Process(T1 input);
    }

    public abstract class AbstractGenericBase<T1, T2> : IGenericService<T1, T2>
    {
        public T2 Process(T1 input)
        {
            throw new NotImplementedException();
        }
    }

    public class GenericServiceAB : AbstractGenericBase<A, B> { }

    [DoNotRegisterService]
    public class GenericServiceBA : AbstractGenericBase<B, A> { }

    public class GenericServiceCEnumerableInt : AbstractGenericBase<C, IEnumerable<int>> { }

    public class B { }

    public class A { }

    public class C { }
}
