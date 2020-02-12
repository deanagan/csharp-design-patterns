using NUnit.Framework;

namespace TemplateMethod.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TaxesApplied_WhenBuyerIsHighIncomeEarner()
        {
            Assert.Fail();
        }

        [Test]
        public void ZeroTaxesApplied_WhenBuyerIsLowIncomeEarner()
        {
            Assert.Fail();
        }
    }
}