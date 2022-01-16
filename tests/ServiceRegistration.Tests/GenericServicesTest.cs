using DependencyInjection.ServiceRegistration.TestAssembly.GenericClasses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DependencyInjection.ServiceRegistration.Tests
{
    public class GenericServicesTest
    {
        private IServiceCollection _sut;

        public GenericServicesTest()
        {
            _sut = new ServiceCollection();
        }

        [Fact]
        public void AddServiceGroup_GenericInterfaceServiceType()
        {
            // Arrange

            // Act
            _sut.AddMultiple(typeof(IGenericService<>));
            var assertion = _sut.Select(x => new { x.ImplementationType, x.ServiceType });

            // Assert
            assertion.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(new[]
                {
                    new
                    {
                        ImplementationType = typeof(GenericServiceA), ServiceType = typeof(IGenericService<A>)
                    },
                    new
                    {
                        ImplementationType = typeof(GenericServiceB), ServiceType  = typeof(IGenericService<B>)
                    }
                });
        }

        [Fact]
        public void AddServiceGroup_GenericInterfaceServiceType_GetRequiredService()
        {
            // Arrange

            // Act
            _sut.AddMultiple(typeof(IGenericService<>));
            var requiredService = _sut.BuildServiceProvider().GetRequiredService<IGenericService<A>>();

            // Assert
            requiredService.Should().BeOfType(typeof(GenericServiceA));

        }

        // DoNotRegisterService
        [Fact]
        public void AddServiceGroup_GenericAbstractClassServiceType()
        {
            // Arrange

            // Act
            _sut.AddMultiple(typeof(AbstractGenericBase<,>));
            var assertion = _sut.Select(x => new { x.ImplementationType, x.ServiceType });

            // Assert
            assertion.Should().NotBeEmpty()
               .And.HaveCount(2)
               .And.BeEquivalentTo(new[]
               {
                    new
                    {
                        ImplementationType = typeof(GenericServiceAB), ServiceType = typeof(AbstractGenericBase<A,B>)
                    },
                    new
                    {
                        ImplementationType = typeof(GenericServiceCEnumerableInt), ServiceType  = typeof(AbstractGenericBase<C, IEnumerable<int>>)
                    }
               });
        }

        [Fact]
        public void AddServiceGroup_GenericAbstractClassServiceType_GetRequiredService()
        {
            // Arrange

            // Act
            _sut.AddMultiple(typeof(AbstractGenericBase<,>));
            var assertion = _sut.Select(x => new { x.ImplementationType, x.ServiceType });

            // Assert
            assertion.Should().NotBeEmpty()
               .And.HaveCount(2)
               .And.BeEquivalentTo(new[]
               {
                    new
                    {
                        ImplementationType = typeof(GenericServiceAB), ServiceType = typeof(AbstractGenericBase<A,B>)
                    },
                    new
                    {
                        ImplementationType = typeof(GenericServiceCEnumerableInt), ServiceType  = typeof(AbstractGenericBase<C, IEnumerable<int>>)
                    }
               });
        }
    }
}
