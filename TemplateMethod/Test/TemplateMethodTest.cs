using NUnit.Framework;
using Moq;
using FluentAssertions;

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
            // Arrange
            var highIncomeBuyer = new HighIncomeBookPriceCalculator(5);
            var book = Mock.Of<IBook>(book => book.Price == 100.0M);

            // Act
            var priceAfterTax = highIncomeBuyer.CalculatePrice(book);

            // Assert
            priceAfterTax.Should().Be(106.4M);
        }

        [Test]
        public void ZeroTaxesApplied_WhenBuyerIsLowIncomeEarner()
        {
            // Arrange
            var lowIncomeBuyer = new LowIncomeBookPriceCalculator(5);
            var book = Mock.Of<IBook>(book => book.Price == 100.0M);

            // Act
            var priceAfterTax = lowIncomeBuyer.CalculatePrice(book);

            // Assert
            priceAfterTax.Should().Be(95.0M);
        }
    }
}