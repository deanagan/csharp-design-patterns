using System.Collections.Generic;
using System.Linq;

namespace MementoPattern
{
    public class ProductCaretaker : IProductCaretaker
    {
        private IList<Product> productMementos = new List<Product>();
        public void AddProductMemento(Product product)
        {
            var memento = new Product { Name = product.Name, Price = product.Price };
            productMementos.Add(memento);
        }

        public Product GetLastMemento()
        {
            return productMementos.DefaultIfEmpty(null).Last();
        }
    }
}
