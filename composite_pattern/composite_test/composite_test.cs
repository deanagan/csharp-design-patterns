using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace Composite.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BookPriceIsCorrect_WhenBookIsPurchasedByItself()
        {
            // Arrange
            var book = new Book("Python Cookbook", 50.0M);

            // Act
            var price = book.Price;

            // Assert
            price.Should().Be(50.0M);
        }
    }
}