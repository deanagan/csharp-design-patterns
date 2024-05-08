using System;

namespace Memento
{
    public class ProductOriginator : IProductOriginator
    {
        private Product? _product;

        public ProductOriginator(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            SetMemento(product);
        }

        public void SetMemento(Product product)
        {
            _product = product;
        }

        public Product GetMemento()
        {
            ArgumentNullException.ThrowIfNull(_product);
            return _product;
        }
    }
}
