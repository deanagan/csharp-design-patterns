using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

using Api.Models;
using Api.Services;
using Api.Interfaces;

namespace Test.Service
{
    public class ProductServiceTest
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1, Price = 1.23M, SkuCode = "PROD_001", Name = "Cool 1"
            },
            new Product
            {
                Id = 2, Price = 4.56M, SkuCode = "PROD_002", Name = "Awesome 2"
            },
        };
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly ILogger<IProductService> _logger;

        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            _productRepositoryMock.Setup(pr => pr.GetProducts()).Returns(_products);
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<IProductService>();
            _productService = new ProductService(_productRepositoryMock.Object, _logger);
        }

        [Fact]
        public void AllProductsReturned_WhenGetProductsInvoked()
        {
            // Arrange

            // Act
            var product = _productService.GetProducts();

            // Assert
            product.Should().Equal(_products);
        }

        [Fact]
        public void CorrectProductReturned_WhenGetProductRetrievedThruId()
        {

            // Act
            var product = _productService.GetProduct(1);

            // Assert
            product.Should().Be(_products.First());
        }

        [Fact]
        public void NullProductReturned_WhenIdDoesNotExist()
        {
            // Act
            var product = _productService.GetProduct(4);

            // Assert
            product.Should().BeNull();
        }

    }
}
