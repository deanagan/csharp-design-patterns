using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Models;
using Api.Interfaces;

namespace Api.Services
{
    public class GetProductService : IProductService
    {
        private IProductRepository productRepository;
        private ILogger logger;
        public GetProductService(IProductRepository productRepository, ILogger logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public Product GetProduct(ProductInfo productInfo)
        {
            var result = productRepository.GetProducts()
                    .Where(product => productInfo.SkuCode == product.SkuCode)
                    .Select(matchingProduct => matchingProduct)
                    .DefaultIfEmpty(null)
                    .First();

            if (result == null)
            {
                logger.LogWarning("Sku code {0} not found.", productInfo.SkuCode);
            }
            return result;
        }

    }
}
