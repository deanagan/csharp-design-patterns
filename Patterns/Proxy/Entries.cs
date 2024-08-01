using System.Collections.Generic;

namespace Proxy
{
    public class Entries : IEntries
    {
        private Dictionary<int, IProductInfo> _products;
        public Entries(Dictionary<int, IProductInfo> products)
        {
            _products = products;
        }

        public bool Delete(int id)
        {
            return _products.Remove(id);
        }
        public IProductInfo? Get(int id)
        {
            if (!_products.ContainsKey(id))
            {
                return null;
            }

            return _products[id];
        }

    }
}
