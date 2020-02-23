using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace ProxyPattern.Tests
{
    public class Tests
    {
        Dictionary<int, IProductInfo> _products;
        [SetUp]
        public void Setup()
        {
            _products = new Dictionary<int, IProductInfo> 
            {
                {1, Mock.Of<IProductInfo>(
                    productInfo => 
                        productInfo.Name == "Xbox360" && 
                        productInfo.Id == 1)
                }
            };
        }

        [Test]
        public void GetProductInfoSuccess_WhenReadingAsRegularUser()
        {
            // Arrange
            var user = Mock.Of<IUser> (user => user.IsAdmin == false);
            var authCtrl = new AuthenticationController(user);
            var entries = new Entries(_products, authCtrl);
            
            // Act
            var productInfo = entries.Get(1);

            //Assert
            productInfo.Id.Should().Be(1);
            productInfo.Name.Should().Be("Xbox360");
        }

        [Test]
        public void GetProductInfoSuccess_WhenReadingAsAdminUser()
        {
            // Arrange
            var user = Mock.Of<IUser> (user => user.IsAdmin == true);
            var authCtrl = new AuthenticationController(user);
            var entries = new Entries(_products, authCtrl);

            // Act
            var productInfo = entries.Get(1);

            //Assert
            productInfo.Id.Should().Be(1);
            productInfo.Name.Should().Be("Xbox360");
        }

        [Test]
        public void DeleteProductInfoSuccess_WhenDeletingAsAdminUser()
        {
            // Arrange
            var user = Mock.Of<IUser> (user => user.IsAdmin == true);
            var authCtrl = new AuthenticationController(user);
            var entries = new Entries(_products, authCtrl);

            // Act
            var isDeleted = entries.Delete(1);

            //Assert
            isDeleted.Should().Be(true);
            entries.Get(1).Should().Be(null);
        }

        [Test]
        public void DeleteProductInfoFailed_WhenDeletingAsRegularUser()
        {
            // Arrange
            var user = Mock.Of<IUser> (user => user.IsAdmin == false);
            var authCtrl = new AuthenticationController(user);
            var entries = new Entries(_products, authCtrl);

            // Act
            var isDeleted = entries.Delete(1);

            //Assert
            isDeleted.Should().Be(false);
            entries.Get(1).Should().NotBe(null);
        }
    }
}