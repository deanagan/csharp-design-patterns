using System;
using System.Collections.Generic;
using System.Linq;

namespace Memento
{
    public class ProductCaretaker : IProductCaretaker
    {
        private IList<Product> productMementos = new List<Product>();
        public void AddProductMemento(Product product)
        {
            productMementos.Add(product.ShallowCopy());
        }

        public Product GetLastMemento()
        {
            var product = productMementos.DefaultIfEmpty(null).Last();

            ArgumentNullException.ThrowIfNull(product);

            return product;
        }
    }
}
