using DependencyInjection.ServiceRegistration.TestAssembly.AttributeDecoratedClasses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace DependencyInjection.ServiceRegistration.Tests
{
    public class DecoratedServicesTest
    {
        private IServiceCollection _sut;

        public DecoratedServicesTest()
        {
            _sut = new ServiceCollection();
        }

        [Fact]
        public void RegisterServices_ClassesDecoratedWithDoNotRegisterServiceAttributeAreNotRegistered()
        {
            // Arrange

            // Act
            _sut.RegisterServices(Assembly.GetAssembly(typeof(IDecoratedService)));
            var serviceCollection = _sut.BuildServiceProvider().GetRequiredService<IEnumerable<IDecoratedService>>();

            var assertion = serviceCollection.Select(x => x.Run()).ToList();

            // Assert
            assertion.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.NotContain(typeof(DecoratedClassC))
                .And.BeEquivalentTo(new[]
                {
                    typeof(DecoratedClassA),
                    typeof(DecoratedClassB),
                    typeof(DirectDecoratedClass)
                });
        }

        [Fact]
        public void RegisterServices_UseCallingAssembly_NoServicesAreRegistered()
        {
            // Arrange

            // Act
            _sut.RegisterServices();
            var serviceCollection = _sut.BuildServiceProvider().GetRequiredService<IEnumerable<IDecoratedService>>();

            // Assert
            serviceCollection.Should().BeEmpty();
        }
    }
}
