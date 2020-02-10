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

        private decimal ComputeDiscount(int discount, decimal price)
        {
            return price - (price * (discount / 100M));
        }

        [Test]
        public void BookPriceIsCorrect_WhenBookIsPurchasedByItself()
        {
            // Arrange
            var book = new Book("Python Cookbook", 50.0M);

            // Act
            var price = ComputeDiscount(book.Discount, book.Price);

            // Assert
            price.Should().Be(50.0M);
        }

        [Test]
        public void BookPriceDiscountedAsSet_WhenBooksArePurchasedUnderBookComposite()
        {
            // Arrange
            var book1 = new Book("Effective C++", 65.0M);
            var book2 = new Book("More Effective C++", 35.0M);
            var bookComposite = new BookComposite(5, "Effective C++ Series");
            bookComposite.Add(book1);
            bookComposite.Add(book2);

            // Act
            var compositePrice = ComputeDiscount(bookComposite.Discount, bookComposite.Price);
            
            // Assert
            compositePrice.Should().Be(95.0M);

        }
    }
}