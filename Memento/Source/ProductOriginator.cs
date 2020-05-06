using System;

namespace Memento
{
    public class ProductOriginator : IProductOriginator
    {
        private Product product;

        public ProductOriginator(Product product)
        {
            SetMemento(product);
        }

        public void SetMemento(Product product)
        {
            this.product = product;
        }

        public Product GetMemento()
        {
            return this.product;
        }
    }
}
