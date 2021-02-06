using System.Collections.Generic;

using Api.Interfaces;
using Api.Models;

namespace Api.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> ProductsForSale = new List<Product>
        {
            new Product()
            {
                Id = 1, Price = 12.34M, SkuCode = "PRODUCT_001", Name = "Awesome product"
            },
            new Product()
            {
                Id = 2, Price = 56.78M, SkuCode = "PRODUCT_002", Name = "Cool product"
            },
            new Product()
            {
                Id = 3, Price = 98.76M, SkuCode = "PRODUCT_003", Name = "Fantastic product"
            },
        };

        public List<Product> GetProducts()
        {
            return ProductsForSale;
        }
    }
}