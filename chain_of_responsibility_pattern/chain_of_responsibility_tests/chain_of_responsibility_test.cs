using NUnit.Framework;

namespace chain_of_responsibility_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CorrectProductCodeHandler_ComputesDiscount_WhenMatchedWithProductCode()
        {
            Assert.Pass();
        }

        [Test]
        public void UseDefaultHandler_WhenProductCodeHasNoMatch()
        {
            Assert.Pass();
        }
    }
}