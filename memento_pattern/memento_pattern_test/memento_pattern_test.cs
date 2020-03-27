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
            var product = Mock.Of<Product>(product => product.Name == "Rice" && product.Price == 4.99M);
            var productOriginator = new ProductOriginator(product);
            var productMemento = productOriginator.GetMemento();
            productCaretaker.AddProductMemento(productMemento);
            productMemento.Price = 2.99M;

            // Act
            productOriginator.SetMemento(productCaretaker.GetLastMemento());
            productMemento = productOriginator.GetMemento();

            // Assert
            productMemento.Price.Should().Be(4.99M);
        }
    }
}
