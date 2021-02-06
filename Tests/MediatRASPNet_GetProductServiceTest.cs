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
    public class GetProductServiceTest
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
        private ProductInfo _productInfo = new ProductInfo();
        public GetProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            _productRepositoryMock.Setup(pr => pr.GetProducts()).Returns(_products);
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<IProductService>();
            _productService = new GetProductService(_productRepositoryMock.Object, _logger);
        }

        [Fact]
        public void CorrectProductReturned_WhenGetProductRetrievedThruSkuCode()
        {
            // Arrange
            _productInfo.SkuCode = "PROD_001";

            // Act
            var product = _productService.GetProduct(_productInfo);

            // Assert
            product.Should().Be(_products.First());
        }

        [Fact]
        public void NullProductReturned_WhenSkuCodeDoesNotExist()
        {
            // Arrange
            _productInfo.SkuCode = "PROD_003";

            // Act
            var product = _productService.GetProduct(_productInfo);

            // Assert
            product.Should().BeNull();
        }

    }
}
