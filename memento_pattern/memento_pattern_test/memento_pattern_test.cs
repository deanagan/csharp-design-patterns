using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace MementoPattern.Test
{
    public class Tests
    {
        private ProductCaretaker productCaretaker;
        [SetUp]
        public void Setup()
        {
            productCaretaker = new ProductCaretaker();
        }

        [Test]
        public void CorrectMementoRetrievedWhenRetrievingFromCaretaker()
        {
            // Arrange
            var product = Mock.Of<IProduct>(product => product.Name == "Rice" && product.Price == 4.99M);
            var productOriginator = new ProductOriginator(product);
            var productMemento = productOriginator.CreateMemento();
            productCaretaker.AddProductMemento(productMemento);
            product.Price = 2.99M;

            // Act
            productOriginator.SetMemento(productCaretaker.GetLastMemento());

            // Assert
            product.Price.Should().Be(4.99M);
        }
    }
}
