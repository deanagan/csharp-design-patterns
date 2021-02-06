using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Api.Models;
using Api.Interfaces;
using Api.Controllers;

namespace Test.Controller
{
    public class ProductControllerTest
    {
        private readonly ILogger<ProductController> _logger;
        private readonly Product _product = new Product
        {
            Id = 1, Price = 1.99M, SkuCode = "PROD_001", Name = "Cheap product"
        };

        private readonly List<string> _productSkuCodes = new List<string>
        { "PROD_001", "PROD_002", "PROD_003" };

        public ProductControllerTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<ProductController>();
        }
        [Fact]
        public void ProductShouldBeReturned_WhenItExists()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            productService.Setup(s => s.GetProduct(It.IsAny<ProductInfo>())).Returns(_product);
            var controller = new ProductController(productService.Object, _logger);

            // Act
            var response = controller.GetProduct(It.IsAny<ProductInfo>()) as ObjectResult;

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().Be(_product);
        }


        [Fact]
        public void Error404Returned_WhenGettingNonExistentProduct()
        {
            var productService = new Mock<IProductService>();
            productService.Setup(s => s.GetProduct(It.IsAny<ProductInfo>())).Returns(null as Product);
            var controller = new ProductController(productService.Object, _logger);

            var response = controller.GetProduct(It.IsAny<ProductInfo>()) as ObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
