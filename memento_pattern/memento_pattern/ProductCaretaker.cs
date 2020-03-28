using System.Collections.Generic;
using System.Linq;

namespace MementoPattern
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
            return productMementos.DefaultIfEmpty(null).Last();
        }
    }
}
