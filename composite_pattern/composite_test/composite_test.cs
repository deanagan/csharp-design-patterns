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

        [Test]
        public void BookPriceDiscountedWithTopSet_WhenBooksArePurchasedUnderBookCompositeWithAnotherComposite()
        {
            // Arrange
            var book1 = new Book("Effective C++", 65.0M);
            var book2 = new Book("More Effective C++", 35.0M);
            var bookCompositeCpp = new BookComposite(5, "Effective C++ Series");
            bookCompositeCpp.Add(book1);
            bookCompositeCpp.Add(book2);

            var bookCompositeCSharp = new BookComposite(10, "Effective C# Series");
            var book3 = new Book("Effective C#", 70.0M);
            var book4 = new Book("Pro C# Coding", 30.0M);

            bookCompositeCSharp.Add(book3);
            bookCompositeCSharp.Add(book4);

            var effectiveSeries = new BookComposite(15, "Effective Coding Series");
            effectiveSeries.Add(bookCompositeCpp);
            effectiveSeries.Add(bookCompositeCSharp);

            // Act
            var effectiveSeriesPrice = ComputeDiscount(effectiveSeries.Discount, effectiveSeries.Price);
            
            // Assert
            effectiveSeriesPrice.Should().Be(170.0M);

        }
    }
}