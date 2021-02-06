using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Models;
using Api.Interfaces;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        private ILogger logger;
        public ProductService(IProductRepository productRepository, ILogger logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public List<string> GetAllSkuCodes()
        {
            logger.LogTrace("Service Get All Sku Codes Invoked.");
            return productRepository.GetProducts().Select(product => product.SkuCode).ToList();
        }

        public Product GetProduct(string skuCode)
        {
            var result = productRepository.GetProducts().Where(product => product.SkuCode == skuCode)
                    .Select(matchingProduct => matchingProduct)
                    .DefaultIfEmpty(null)
                    .First();
            if (result == null)
            {
                logger.LogWarning("Sku code {0} not found.", skuCode);
            }
            return result;
        }

    }
}
