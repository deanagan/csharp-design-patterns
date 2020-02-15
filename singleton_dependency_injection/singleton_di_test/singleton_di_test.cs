using NUnit.Framework;
using Moq;
using FluentAssertions;
using SingletonDi;

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
    }
}