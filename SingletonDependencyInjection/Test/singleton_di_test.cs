using NUnit.Framework;
using Moq;
using FluentAssertions;
using Ninject;

namespace SingletonDi.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InstanceIsSame_WhenComparingGoFSingleton()
        {
            var logger1 = GoFLogger.Instance;
            var logger2 = GoFLogger.Instance;

            logger1.Should().BeSameAs(logger2);
        }

        [Test]
        public void InstanceIsNotSame_WhenComparingNotInSingletonScope()
        {
            var container = new StandardKernel();
            container.Bind<ILogger>().To<Logger>();

            var logger1 = container.Get<ILogger>();
            var logger2 = container.Get<ILogger>();

            logger1.Should().NotBeSameAs(logger2);           
        }

        [Test]
        public void InstanceIsSame_WhenComparingInSingletonScope()
        {
            var container = new StandardKernel();
            container.Bind<ILogger>().To<Logger>().InSingletonScope();

            var logger1 = container.Get<ILogger>();
            var logger2 = container.Get<ILogger>();

            logger1.Should().BeSameAs(logger2);
        }
    }
}