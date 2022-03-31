using System;
using Cygnus.ConsoleApp.Tasks;
using Cygnus.Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cygnus.ConsoleApp.UnitTests.Tasks
{
    [Trait("Category", "UnitTests")]
    public class ApplicationTaskFactoryTest
    {
        [Fact]
        public void ApplicationTaskFactoryCreate_WhenInvalidProviderModel_ShouldThrowException()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var factory = new ApplicationTaskFactory(serviceProvider);
            var model = new EntityModel
            {
                Source = new()
                {
                    ProviderType = ProviderTypeModel.None
                },
                Destination = new()
                {
                    ProviderType = ProviderTypeModel.None
                }
            };

            // Act
            var exc = Assert.Throws<InvalidOperationException>(() => factory.Create(model));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Operation \"None\" -> \"None\" not implemented");
        }
    }
}
