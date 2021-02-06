using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using Api.Models;
using Api.Interfaces;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ILogger<IProductService> _logger;
        public ProductService(IProductRepository productRepository, ILogger<IProductService> logger)
        {
            this._productRepository = productRepository;
            this._logger = logger;
        }

        public List<Product> GetProducts()
        {
            var result = _productRepository.GetProducts();

            if (result == null)
            {
                _logger.LogWarning("No products");
            }

            return result;
        }

        public Product GetProduct(int id)
        {
            var result = _productRepository.GetProducts()
                    .Where(product => id == product.Id)
                    .Select(matchingProduct => matchingProduct)
                    .DefaultIfEmpty(null)
                    .First();

            if (result == null)
            {
                _logger.LogWarning("Product with id = {0} not found.", id);
            }
            return result;
        }

    }
}
