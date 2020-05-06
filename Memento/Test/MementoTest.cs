using Xunit;
using Moq;
using FluentAssertions;

namespace Memento.Test
{
    public class MementoShould
    {
        private ProductCaretaker _productCaretaker = new ProductCaretaker();
        private Product _product = Mock.Of<Product>(product => product.Name == "Rice" && product.Price == 4.99M);
        private IProductOriginator _productOriginator;
        private Product _memento;

        public MementoShould()
        {
            _productOriginator = new ProductOriginator(_product);
            _memento = _productOriginator.GetMemento();
        }
        [Fact]
        public void RetrieveCorrectMemento_WhenRetrievingFromCaretaker()
        {
            // Arrange
            _productCaretaker.AddProductMemento(_memento);

            // Act
            _memento.Price = 2.99M;
            _productOriginator.SetMemento(_productCaretaker.GetLastMemento());
            _memento = _productOriginator.GetMemento();

            // Assert
            _memento.Price.Should().Be(4.99M);
        }

        [Fact]
        public void RetrieveCorrectMemento_WhenRetrievingFromCaretakerAfterMultipleSet()
        {
            // Arrange
            _productCaretaker.AddProductMemento(_memento);

            // Act
            _memento.Price = 2.99M;
            _productCaretaker.AddProductMemento(_memento);
            _memento.Price = 7.99M;
            _productOriginator.SetMemento(_productCaretaker.GetLastMemento());
            _memento = _productOriginator.GetMemento();

            // Assert
            _memento.Price.Should().Be(2.99M);
        }
    }
}
