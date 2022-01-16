using DependencyInjection.ServiceRegistration.TestAssembly.Classes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DependencyInjection.ServiceRegistration.Tests
{
    public class CommonServicesTest
    {
        private IServiceCollection _sut;

        public CommonServicesTest()
        {
            _sut = new ServiceCollection();
        }

        [Fact]
        public void AddServiceGroup_InterfaceServiceType_ClassesDecoratedWithDoNotRegisterServiceAttributeAreNotRegistered()
        {
            // Arrange

            // Act
            _sut.AddMultiple(typeof(IService));
            var serviceCollection = _sut.BuildServiceProvider().GetRequiredService<IEnumerable<IService>>();

            var assertion = serviceCollection.Select(x => x.Run());

            // Assert
            assertion.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.NotContain(typeof(ServiceD))
                .And.BeEquivalentTo(new[]
                {
                    typeof(ServiceA),
                    typeof(ServiceB),
                    typeof(ServiceC)
                });
        }

        [Fact]
        public void AddServiceGroup_AbstractClassServiceType()
        {
            // Arrange

            // Act
            _sut.AddMultiple(typeof(AbstractBase));
            var serviceCollection = _sut.BuildServiceProvider().GetRequiredService<IEnumerable<AbstractBase>>();

            var assertion = serviceCollection.Select(x => x.Run());

            // Assert
            assertion.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(new[]
                {
                    typeof(ServiceA),
                    typeof(ServiceB),
                });
        }
    }
}
