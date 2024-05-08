using Xunit;
using Moq;
using FluentAssertions;
using Ninject;

namespace SingletonDI.Test
{
    public class SingletonDIShould
    {

        [Fact]
        public void ReturnSameInstance_WhenComparingLazyLoggerSingleton()
        {
            // Arrange
            var logger1 = Logger.Instance;
            // Act
            var logger2 = Logger.Instance;
            // Assert
            logger1.Should().BeSameAs(logger2);
        }

        [Fact]
        public void ReturnDifferentInstance_WhenComparingInstanceCreatedByNinjectInTransientScope()
        {
            // Arrange
            var container = new StandardKernel();
            container.Bind<ILogger>().To<Logger>();
            // Act
            var logger1 = container.Get<ILogger>();
            var logger2 = container.Get<ILogger>();
            // Assert
            logger1.Should().NotBeSameAs(logger2);
        }

        [Fact]
        public void ReturnSameInstance_WhenComparingInstanceCreatedByNinjectInSingletonScope()
        {
            // Arrange
            var container = new StandardKernel();
            container.Bind<ILogger>().To<Logger>().InSingletonScope();
            // Act
            var logger1 = container.Get<ILogger>();
            var logger2 = container.Get<ILogger>();
            // Assert
            logger1.Should().BeSameAs(logger2);
        }
    }
}
