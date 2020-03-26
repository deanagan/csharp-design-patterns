using System;

namespace MementoPattern
{
    public class ProductOriginator : IProductOriginator
    {
        private IProduct product;

        public ProductOriginator(IProduct product)
        {
            SetMemento(product);
        }

        public void SetMemento(IProduct product)
        {
            this.product = product;
        }

        public IProduct CreateMemento()
        {
            return this.product;
        }
    }
}
